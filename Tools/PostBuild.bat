@echo off
:: postbuild merge dllexport batch job

setlocal

:: ILMerge �ւ̃p�X
set ilmerge="D:\Tools\ILMerge\ILMerge.exe"

:: DllExporter �ւ̃p�X
set dllexporter="%~dp0\DllExporter.exe"

:: �쐬�����ƃf�B���N�g����
set createTemp=tmp

:: ��� (library|exe|winexe)
set target=library

:: �^�[�Q�b�g �t���[�����[�N (v1|v1.1|v2|v4)
set targetplatform=v4

:: ���̑��̃A�Z���u��
set otherAssemblies=*.dll

:: ���̑��� ILMerge �I�v�V����
set ilmergeOptions=/wildcards

if "%*" equ "" (
	echo Usage: %~n0 [/debug] [/copyto:dest] ^<primary assembly^>

	exit /b
)

set targetPath=
set targetName=
set targetNameExt=
set configuration=release
set copyTo=

call :parseArguments %*

call :initializeTempDirectory
	call :ilmerge
	call :export
call :cleanTempDirectory

if "%copyTo%" neq "" copy /y %targetPath% %copyTo%

exit /b

::::

:parseArguments
	:parseArgumentsLoop
		if "%1" equ "" exit /b
	
		set iSubstring=%1

		if "%iSubstring:~0,1%" equ "/" (
			if /i "%iSubstring:~1%" equ "debug" (
				set configuration=debug
			) else if /i "%iSubstring:~1,7%" equ "copyto:" (
				set copyTo=%iSubstring:~8%
			)
		) else if "%1" neq "" (
			set targetPath=%1
			set targetName=%~n1
			set targetNameExt=%~nx1
		)

		shift /1
	goto :parseArgumentsLoop
exit /b

:ilmerge
	%ilmerge% /t:%target% /%targetplatform% /out:%createTemp%\%targetNameExt% %1 %otherAssemblies% %ilmergeOptions%
	copy /y %createTemp%\%targetNameExt% > nul
	if exist %createTemp%\%targetName%.pdb copy /y %createTemp%\%targetName%.pdb > nul
	
	for %%i in (*.dll;*.pdb) do (
		if "%%~ni" neq "%targetName%" del %%i
	)
exit /b

:export
	%dllexporter% /%configuration% %targetPath%
exit /b

:initializeTempDirectory
	if not exist %createTemp% mkdir %createTemp%
	del /q %createTemp%\*
exit /b

:cleanTempDirectory
	rmdir /s /q %createTemp%
exit /b