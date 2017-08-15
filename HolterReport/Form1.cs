using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolterReport
{
	public partial class Form1 : Form
	{
		string[] strs = new string[5] { "", "", "", "", "" };
		public Form1()
		{
			InitializeComponent();
			this.TopMost = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			txt1.Text = "";
			txt2.Text = "";
			txt3.Text = "";
			txt4.Text = "";
			txt5.Text = "";
			strs[0] = "";
			strs[1] = "";
			strs[2] = "";
			strs[3] = "";
			strs[4] = "";
			Encoding enc = Encoding.GetEncoding("Shift_JIS");
			int num = enc.GetByteCount(txtmain.Text);
			if (num > 64 * 5)
				MessageBox.Show("文字数オーバー");

			string[] strstmp = mbStrSplit(txtmain.Text, 64);
			for (int i = 0; i < strstmp.Count(); i++ )
			{
				strs[i] = strstmp[i];
			}
			txt1.Text = strs[0];
			txt2.Text = strs[1];
			txt3.Text = strs[2];
			txt4.Text = strs[3];
			txt5.Text = strs[4];
			

		}
		//*********************************************************************
		/// <summary>文字列を指定した文字数単位で分割する(全角文字考慮)
		/// </summary>
		/// <param name="inStr">  分割前文字列</param>
		/// <param name="length"> 1行の長さ</param>
		/// <returns>             分割後文字列の配列</returns>
		//*********************************************************************
		private string[] mbStrSplit(string inStr, int length)
		{
			List<string> outArray = new List<string>(); // 分割結果の保存領域
			string outStr = "";                 // 現在処理中の分割後文字列
			Encoding enc = Encoding.GetEncoding("Shift_JIS");


			// パラメータチェック
			if (inStr == null || length < 1)
			{
				return outArray.ToArray();
			}

			//--------------------------------------
			// 全ての文字を処理するまで繰り返し
			//--------------------------------------
			for (int offset = 0; offset < inStr.Length; offset++)
			{
				//----------------------------------------------------------
				// 今回処理する文字と、その文字を含めた分割後文字列長を取得
				//----------------------------------------------------------
				string curStr = inStr[offset].ToString();
				int curTotalLength = enc.GetByteCount(outStr) + enc.GetByteCount(curStr);

				//-------------------------------------
				// この文字が、分割点になるかチェック
				//-------------------------------------
				if (curTotalLength == length)
				{
					// 処理中の文字を含めると、ちょうどピッタリ
					outArray.Add(outStr + curStr);
					outStr = "";
				}
				else if (curTotalLength > length)
				{
					// 処理中の文字を含めると、あふれる
					outArray.Add(outStr);
					outStr = curStr;
				}
				else
				{
					// 処理中の文字を含めてもまだ余裕あり
					outStr += curStr;
				}
			}

			// 最後の行の文を追加する
			if (!outStr.Equals(""))
			{
				outArray.Add(outStr);
			}

			// 分割後データを配列に変換して返す
			return outArray.ToArray();
		}

		private void btn1_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txt1.Text);
		}

		private void btn2_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txt2.Text);
		}

		private void btn3_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txt3.Text);
		}

		private void btn4_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txt4.Text);
		}

		private void btn5_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txt5.Text);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			txtmain.Text = "";
			txt1.Text = "";
			txt2.Text = "";
			txt3.Text = "";
			txt4.Text = "";
			txt5.Text = "";
		}

		private void txtmain_TextChanged(object sender, EventArgs e)
		{
			Encoding enc = Encoding.GetEncoding("Shift_JIS");
			int num = enc.GetByteCount(txtmain.Text);

			label1.Text = "あと" + (64 * 5 - num).ToString() + "byte";
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			label1.Text = "あと" + (64 * 5).ToString() + "byte";
		}
	}


}
