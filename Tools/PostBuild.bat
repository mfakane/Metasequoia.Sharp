@echo off
:: postbuild merge dllexport batch job

setlocal

:: ILMerge へのパス
set ilmerge="D:\Tools\ILMerge\ILMerge.exe"

:: DllExporter へのパス
set dllexporter="%~dp0\DllExporter.exe"

:: 作成する作業ディレクトリ名
set createTemp=tmp

:: 種類 (library|exe|winexe)
set target=library

:: ターゲット フレームワーク (v1|v1.1|v2|v4)
set targetplatform=v4

:: その他のアセンブリ
set otherAssemblies=*.dll

:: その他の ILMerge オプション
set ilmergeOptions=/wildcards

:: その他の DllExporter オプション
set dllexporterOptions=

if "%*" equ "" (
	echo Usage: %~n0 ^<primary assembly^> [/debug] [/nomerge] [/mergecoreonly] [/copyto:dest]

	exit /b
)

set targetPath=
set targetName=
set targetNameExt=
set configuration=release
set copyTo=
set il=

call :parseArguments %*

call :initializeTempDirectory
	if "%otherAssemblies%" neq "" call :ilmerge
	if not errorlevel 1 call :export
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
				set il=/il
			) else if /i "%iSubstring:~1%" equ "nomerge" (
				set otherAssemblies=
			) else if /i "%iSubstring:~1%" equ "mergecoreonly" (
				set otherAssemblies=Metasequoia.Sharp.dll
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
	set mergeTargetNameExt=%targetNameExt%

	if "%otherAssemblies%" equ "*.dll" set mergeTargetNameExt=
	
	%ilmerge% /t:%target% /%targetplatform% /out:%createTemp%\%targetNameExt% %mergeTargetNameExt% %otherAssemblies% %ilmergeOptions%

	if errorlevel 1 (
		echo error from ILMerge, batch job failed

		exit /b
	)

	copy /y %createTemp%\%targetNameExt% > nul
	if exist %createTemp%\%targetName%.pdb copy /y %createTemp%\%targetName%.pdb > nul
	
	::for %%i in (*.dll;*.pdb) do (
	for %%i in (%otherAssemblies%) do (
		if "%%~ni" neq "%targetName%" del %%i
	)
exit /b

:export
	%dllexporter% /%configuration% /%targetplatform% %il% %dllexporterOptions% %targetPath%

	if errorlevel 1 (
		echo error from DllExporter, batch job failed

		exit /b
	)
exit /b

:initializeTempDirectory
	if not exist %createTemp% mkdir %createTemp%
	del /q %createTemp%\*
exit /b

:cleanTempDirectory
	rmdir /s /q %createTemp%
exit /b