namespace Metasequoia
{
	public enum CursorType
	{
		/// <summary>
		/// 4 方向矢印
		/// </summary>
		SizeAll = -22,
		/// <summary>
		/// 指先
		/// </summary>
		HandPoint = -21,
		/// <summary>
		/// 疑問符付き
		/// </summary>
		Help = -20,
		/// <summary>
		/// 禁止
		/// </summary>
		No = -18,
		/// <summary>
		/// 砂時計
		/// </summary>
		Wait = -11,
		/// <summary>
		/// 上向き矢印
		/// </summary>
		UpArrow = -10,
		/// <summary>
		/// 左右を指す両方向矢印
		/// </summary>
		SizeWE = -9,
		/// <summary>
		/// 左上と右下を指す両方向矢印
		/// </summary>
		SizeNWSE = -8,
		/// <summary>
		/// 上下を指す両方向矢印
		/// </summary>
		SizeNS = -7,
		/// <summary>
		/// 右上と左下を指す両方向矢印
		/// </summary>
		SizeNESW = -6,
		/// <summary>
		/// 十字
		/// </summary>
		Cross = -3,
		/// <summary>
		/// 非表示
		/// </summary>
		None = -1,
		/// <summary>
		/// 標準
		/// </summary>
		Default = 0,
		/// <summary>
		/// 範囲
		/// </summary>
		Rect = 1,
		/// <summary>
		/// 投げ縄
		/// </summary>
		Rope = 2,
		/// <summary>
		/// 移動
		/// </summary>
		Move = 3,
		/// <summary>
		/// 拡大
		/// </summary>
		Scale = 4,
		/// <summary>
		/// 回転
		/// </summary>
		Rotate = 5,
	}
}
