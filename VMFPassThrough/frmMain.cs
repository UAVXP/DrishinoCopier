using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;

// For localization
using System.Globalization;
using System.Threading;
using System.Resources;

namespace VMFPassThrough
{
	public partial class frmMain : Form
	{
		public string appLanguage = Properties.Settings.Default.Language;
		public frmMain()
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(appLanguage);
			InitializeComponent();
		}

		// TODO: https://www.studentcompanion.co.za/creating-multilingual-applications-with-c/
		private void ChangeLanguage(string lang)
		{
			foreach (Control c in this.Controls)
			{
				ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
				resources.ApplyResources(c, c.Name, new CultureInfo(lang));
			}
		}

		private void Log_Print(string text)
		{
			int start = rtbLog.SelectionStart;
			int length = rtbLog.SelectionLength;

		//	rtbLog.Text += text;
			rtbLog.AppendText(text);
			rtbLog.Select(rtbLog.MaxLength, 0);
			rtbLog.ScrollToCaret();
			rtbLog.Select(start, length);
		}
		private void ColorPrint(string text, Color color)
		{
			rtbLog.Select(0, 0);
			int startPosition = rtbLog.Text.Length;
			Log_Print(text);
			rtbLog.Select(startPosition, text.Length);
			rtbLog.SelectionColor = color;
			rtbLog.Select(0, 0);
		}

		public void Warning(string text)
		{
			text += "\r\n";
			ColorPrint(text, Color.Red);
		}
		public void Msg(string text, bool success = false)
		{
			text += "\r\n";
			if (success)
				ColorPrint(text, Color.ForestGreen); // LimeGreen
			else
				ColorPrint(text, SystemColors.WindowText);
		}

		List<Asset> allAssets = new List<Asset>();

		bool GetValue(string line, string forSearch, out string value)
		{
			value = "";

			try
			{
				line = line.Trim();
				int forSearchLength = forSearch.Length;
				int findMaterial = line.IndexOf(forSearch);
				if (findMaterial < 0) // Если forSearch в строке нет
					return false;
				if (findMaterial > 0) // Если forSearch находится дальше, чем 0-й символ от начала строки, например: "name" "model" - здесь это просто значение, а не название параметра
					return false;

				//	value = line.Substring(forSearchLength + 1).Trim(trimChars: '"').ToLower();

				string prefix = "";
				string postfix = "";
				switch (forSearch)
				{
					case "\"detailmaterial\"":
					case "\"material\"":
						prefix = "materials\\";
						postfix = ".vmt";
						break;
					case "\"model\"":
						break;
					case "\"soundname\"":
						prefix = "sound\\";
						break;
					default:
						break;
				}
				string naked = line.Substring(forSearchLength + 1).Replace('/', '\\').Trim(trimChars: '"').ToLower();

				if (naked == forSearch) // i.e. if visgroup named after "model". Is this makes sense?
				{
					return false;
				}
				value = prefix + naked + postfix;

				// If not duplicated - then add
				/*	if (allAssets.Contains(value))
						return false;

					int dummy = 0;
					if (int.TryParse(value, out dummy))
						return false;

					allAssets.Add(value);
					return true;*/
				return AddToAllAssets(value, naked);
			}
			catch { }

			return false;
		}
		private bool AddToAllAssets(string value, string naked = "")
		{
			// If not duplicated - then add
		//	if (allAssets.Contains(value))
		//		return false;
		//	if(allAssets.Find(asset => asset.Contains(value)))
			if (allAssets.Exists(tmpasset => tmpasset.Content == value))
				return false;

			// Check for digit
			int dummy = 0;
			if (int.TryParse(naked, out dummy))
				return false;

			// Add to all assets
			AssetType type = AssetType.Undefined;
			if (value.EndsWith(".vmt")) // Material
			{
				type = AssetType.Material;
			}
			else if (value.EndsWith(".mdl")) // Model
			{
				type = AssetType.Model;
			}
			else if (value.EndsWith(".wav") || value.EndsWith(".mp3")) // Sound
			{
				type = AssetType.Sound;
			}
			Asset asset = new Asset(type, value, value);
			allAssets.Add(asset);
			return true;
		}

		bool GetMaterialValue(string line, string forSearch, out string value)
		{
			value = "";

			try
			{
				line = line.Trim();
				line = line.ToLower(); // Fix for $baseTexture, for example

				int forSearchLength = forSearch.Length;
				int findMaterial = line.IndexOf(forSearch);
				if (findMaterial < 0) // Если forSearch в строке нет
					return false;

				// В парсере vmt это не нужно
			//	if (findMaterial > 0) // Если forSearch находится дальше, чем 0-й символ от начала строки, например: "name" "model" - здесь это просто значение, а не название параметра
			//		return false;

				string naked = line.Substring(forSearchLength + 1).Replace('/', '\\').ToLower();

				if (naked == forSearch) // Нужно ли это для материалов?
					return false;

				if (naked.StartsWith("_")) // _rt* material
					return false;

				string test1 = naked.Substring(forSearch.Length);
				int test2 = test1.IndexOf(' ');
			//	Console.WriteLine("{0} - {1}", test1, test2);

				if (test2 > 0) // Если пробел после параметра не находится сразу после него, а не как например в $basetexturetransform - то не продолжать
					return false;

				naked = naked.Trim(trimChars: new char[] { '"', '\"', '\t', ' ' });

				value = naked;

				return true;
			}
			catch { }

			return false;
		}

		string g_fileName = "";
		string g_folderSourceName = "";
		string g_folderDestName = "";

		private void AddMaterialsFromModel(MDLReader mdlreader, string modpath, string value, out List<string> folders, out List<string> files, out int version)
		{
			mdlreader.ReadTexturePaths(modpath + "\\" + value, out folders, out files, out version);

			if (version > 0)
				AddListResources("model", value + String.Format(" ({0} version)", version));
			else
			{
				//	AddListResources("model", value + " (ERROR reading)");
				Warning("Ошибка при чтении файла " + modpath + "\\" + value);
			}

			// Правильно ли здесь всё?
			foreach (string texturePath in folders)
			{
				foreach (string texture in files)
				{
					string fullTexturePath = "materials\\" + texturePath + texture + ".vmt";
				//	lbMaterials.Items.Add(fullTexturePath + " (model texture)");
					AddListResources("material", fullTexturePath + " (model texture)");
					//	AddToAllAssets(value);
					AddToAllAssets(fullTexturePath);
				}
			}
		}

		private void AddListResources(string resourcetype, string value)
		{
			switch (resourcetype)
			{
				case "material":
					if (lbMaterials.Items.Contains(value)) // If not duplicated - then add
						return;
					lbMaterials.Items.Add(value);
					break;
				case "model":
					if (lbModels.Items.Contains(value)) // If not duplicated - then add
						return;
					lbModels.Items.Add(value);
					break;
				case "sound":
					if (lbSounds.Items.Contains(value)) // If not duplicated - then add
						return;
					lbSounds.Items.Add(value);
					break;
				default:
					throw new Exception("Cannot add unknown resource type to lists!");
			}
		}

		private void LoadMapFile()
		{
			if (g_fileName == "")
			{
				MessageBox.Show("Файл не выбран!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// Cleaning out
			lbMaterials.Items.Clear();
			lbModels.Items.Clear();
			lbSounds.Items.Clear();
			allAssets.Clear();

			Msg("Загружается " + g_fileName);

			string[] file = new string[]{};
			try
			{
				file = File.ReadAllLines(g_fileName);
			}
			catch { Warning("Невозможно прочитать файл"); return; }

			foreach (string line in file)
			{
				string value = "";

				bool foundDetail = GetValue(line, "\"detailmaterial\"", out value);
				if (foundDetail)
					AddListResources("material", value); // lbMaterials.Items.Add(value);

				bool foundMaterial = GetValue(line, "\"material\"", out value);
				if (foundMaterial)
					AddListResources("material", value); // lbMaterials.Items.Add(value);

				bool foundModel = GetValue(line, "\"model\"", out value); // В env_sprite так же зовётся свойство с текстурой спрайта... Fail
				if (foundModel)
				{
					MDLReader mdlreader = new MDLReader();
					List<string> folders = new List<string>();
					List<string> files = new List<string>();
					int version = -1;

					AddMaterialsFromModel(mdlreader, g_folderSourceName, value, out folders, out files, out version);

					if (cbCheckHL2Folder.Checked) // Делать проверку ещё и в основной папке движка Source - hl2
					{
					/* WTF?
						string hl2folder = "";
						hl2folder = g_folderSourceName.Substring(g_folderSourceName.LastIndexOf('\\') + 1);
						Console.WriteLine("Materials: " + hl2folder);
						if (hl2folder != "hl2") // Если выбранная папка-источник уже и так hl2 - не нужно ничего делать в этом случае
						{
							//	mdlreader.ReadTexturePaths(g_folderSourceName + "\\" + value, out folders, out files, out version);
							AddMaterialsFromModel(mdlreader, hl2folder, value, out folders, out files, out version);
						}
					*/
						if (!g_folderSourceName.EndsWith("hl2"))
						{
							string rootfolder = g_folderSourceName.Substring(0, g_folderSourceName.LastIndexOf('\\') + 1);
							AddMaterialsFromModel(mdlreader, rootfolder + "hl2", value, out folders, out files, out version);
						}
					}
				}

				bool foundSound = GetValue(line, "\"soundname\"", out value);
				if (foundSound)
					AddListResources("sound", value); // lbSounds.Items.Add(value);
			}

			tpMaterials.Text =	String.Format("Текстуры ({0})",	lbMaterials.Items.Count);
			tpModels.Text =		String.Format("Модели ({0})",	lbModels.Items.Count);
			tpSounds.Text =		String.Format("Звуки ({0})",	lbSounds.Items.Count);

			Msg("Ресурсы загружены");
		}

		private void LoadMaterialFile(string materialName, out List<string> vmtMaterials)
		{
			vmtMaterials = new List<string>();
			if (materialName == "")
			{
				return;
			}
			// Cleaning out
			vmtMaterials.Clear();

			string[] file = new string[] { };
			try
			{
				file = File.ReadAllLines(materialName);
			}
			catch { }

			foreach (string line in file)
			{
				string value = "";

				bool foundBaseTexture = GetMaterialValue(line, "$basetexture", out value);
				if (foundBaseTexture)
					vmtMaterials.Add(value);

				bool foundBaseTexture2 = GetMaterialValue(line, "$basetexture2", out value);
				if (foundBaseTexture2)
					vmtMaterials.Add(value);

				bool foundEnvMap = GetMaterialValue(line, "$envmap", out value);
				if (foundEnvMap)
					vmtMaterials.Add(value);

				bool foundEnvmapMask = GetMaterialValue(line, "$envmapmask", out value);
				if (foundEnvmapMask)
					vmtMaterials.Add(value);

				bool foundBumpmap = GetMaterialValue(line, "$bumpmap", out value);
				if (foundBumpmap)
					vmtMaterials.Add(value);

				bool foundNormalmap = GetMaterialValue(line, "$normalmap", out value);
				if (foundNormalmap)
					vmtMaterials.Add(value);

				bool foundRefractTexture = GetMaterialValue(line, "$refracttexture", out value);
				if (foundRefractTexture)
					vmtMaterials.Add(value);

				bool foundReflectTexture = GetMaterialValue(line, "$reflecttexture", out value);
				if (foundReflectTexture)
					vmtMaterials.Add(value);

				bool foundBottomMaterial = GetMaterialValue(line, "$bottommaterial", out value);
				if (foundBottomMaterial)
					vmtMaterials.Add(value);

				bool foundToolTexture = GetMaterialValue(line, "%tooltexture", out value);
				if (foundToolTexture)
					vmtMaterials.Add(value);

				/// ?? animatedtexturevar, texturescrollvar
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Обработка файла...";
			DisableAllControls(true);
			LoadMapFile();
			DisableAllControls(false);
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Готово";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			/*DialogResult dr =*/ openFileDialog1.ShowDialog();
		/*	if (dr == System.Windows.Forms.DialogResult.OK) // Maybe callback?
			{
				fileName = openFileDialog1.FileName;
				tbFilePath.Text = fileName;
				UpdateFromFile();
			}*/
		}
		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			g_fileName = openFileDialog1.FileName;
			tbFilePath.Text = g_fileName;
			LoadMapFile();
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Готово";
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Откройте vmf-файл, нажав на кнопку Открыть...";

			tbFilePath.Text = g_fileName;
			tbSourceFolder.Text = g_folderSourceName;
			tbDestFolder.Text = g_folderDestName;

#if DEBUG
			openFileDialog1.InitialDirectory = Path.GetDirectoryName(g_fileName);
			folderBrowserDialog1.SelectedPath = g_folderSourceName;
			folderBrowserDialog2.SelectedPath = g_folderDestName;

			LoadMapFile();
#endif
		//	Console.WriteLine("New Machine ID: " + NewSecurity.GetMachineID());
		}

		private void button4_Click(object sender, EventArgs e)
		{
			DialogResult dr = folderBrowserDialog1.ShowDialog();
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				g_folderSourceName = folderBrowserDialog1.SelectedPath;
				tbSourceFolder.Text = g_folderSourceName;
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			DialogResult dr = folderBrowserDialog2.ShowDialog();
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				g_folderDestName = folderBrowserDialog2.SelectedPath;
				tbDestFolder.Text = g_folderDestName;
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			g_folderSourceName = (sender as TextBox).Text;
			folderBrowserDialog1.SelectedPath = g_folderSourceName;
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			g_folderDestName = (sender as TextBox).Text;
			folderBrowserDialog2.SelectedPath = g_folderDestName;
		}

		private void tbFilePath_TextChanged(object sender, EventArgs e)
		{
			g_fileName = (sender as TextBox).Text;
			openFileDialog1.InitialDirectory = Path.GetDirectoryName(g_fileName);
		}

		void DisableAllControls(bool disable)
		{
			foreach (Control ctr in this.Controls)
			{
				if (ctr.GetType() == typeof(Button) || ctr.GetType() == typeof(TextBox))
				{
					ctr.Enabled = !disable;
				}
			}
		}
		private void button3_Click(object sender, EventArgs e)
		{
			if (allAssets.Count <= 0)
			{
				MessageBox.Show("Список файлов пуст! Возможно, файл не был открыт, либо на карте не используется ни один ассет. Пожалуйста, откройте другой vmf-файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DisableAllControls(true);
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Обработка...";
			toolStripProgressBar1.Value = 0;
			toolStripProgressBar1.Visible = true;

			backgroundWorker1.RunWorkerAsync(new string[] { g_folderSourceName, g_folderDestName });
		/* Testing
			List<string> mats = new List<string>();
			LoadMaterialFile(@"D:\AHL2_R\hl2\materials\Nature\water_wasteland002b.vmt", out mats);
			foreach (string mat in mats)
			{
				Console.WriteLine(mat);
			}
		*/

			// At RunWorkerCompleted
		//	DisableAllControls(false);
		//	statusStrip1.Items["toolStripStatusLabel1"].Text = "Готово";
		}

		private void WorkerRoutine(string folderSourceName, string folderDestName)
		{
			int processedItemCount = 0;
			foreach (Asset asset in allAssets)
			{
				
				Console.WriteLine("Working on " + asset.Content);

				// For materials - copy .vmt, .vtf
				// For models - copy .dx80.vtx, .dx90.vtx, .mdl, .phy, .vvd
				// For sounds - copy only .wav/.mp3

				/// TODO: Проверка на год
				DateTime lwt = File.GetLastWriteTime(folderSourceName + "\\" + asset.Content);
				if (lwt.Year < 2004)
					continue;

				string file = asset.Content;

				if (asset.Type == AssetType.Material) // Material
				{
					if (Directory.Exists(Path.GetDirectoryName(folderSourceName + "\\" + file))) // Is source folder exists
					{
						if (!Directory.Exists(Path.GetDirectoryName(folderDestName + "\\" + file))) // If destination folder doesn't exists - create it
						{
							Directory.CreateDirectory(Path.GetDirectoryName(folderDestName + "\\" + file));
						}
						try
						{
							File.Copy(folderSourceName + "\\" + file, folderDestName + "\\" + file, true); // Copy .vmt
						}
						catch { }

						// Теперь должно итак подхватиться из vmt-файла
						//try
						//{
						//    File.Copy(folderSourceName + "\\" + file.Replace(".vmt", ".vtf"), folderDestName + "\\" + file.Replace(".vmt", ".vtf"), true); // Copy .vtf
						//}
						//catch { }

						/// TODO: Make a model file lookup for model's materials
						List<string> mats = new List<string>();
						//	Console.WriteLine("file: " + folderSourceName + "\\" + file);
						LoadMaterialFile(folderSourceName + "\\" + file, out mats);
						//	LoadMaterialFile(@"D:\AHL2_R\hl2\materials\Nature\water_wasteland002b.vmt", out mats);
						if (mats.Count > 0)
							Console.WriteLine("Parsing " + file);

						foreach (string mat in mats)
						{
							Console.WriteLine("Copying \"" + mat + "\" material file");
							string source = folderSourceName + "\\materials\\" + mat.Replace("/", "\\") + ".vmt";
							string destination = folderDestName + "\\materials\\" + mat.Replace("/", "\\") + ".vmt";
							//	Console.WriteLine(source);
							//	Console.WriteLine(destination);
							if (!File.Exists(source))
								continue;

							if (!Directory.Exists(Path.GetDirectoryName(destination))) // If destination folder doesn't exists - create it
							{
								Directory.CreateDirectory(Path.GetDirectoryName(destination));
							}
							try
							{
								File.Copy(source, destination, true); // Copy all the .vmt's
							}
							catch (Exception ex) { Console.WriteLine(ex.Message); }
							try
							{
								Console.WriteLine("COPY SHIT: " + source.Replace(".vmt", ".vtf"));
								File.Copy(source.Replace(".vmt", ".vtf"), destination.Replace(".vmt", ".vtf"), true); // Copy all the .vtf's
							}
							catch (Exception ex) { Console.WriteLine(ex.Message); }
						}
					}
				}
				else if (asset.Type == AssetType.Model) // Model
				{
					// Copy .mdl
					// Replace .mdl with .dx80.vtx
					// Copy .dx80.vtx
					// etc...
					if (Directory.Exists(Path.GetDirectoryName(folderSourceName + "\\" + file))) // Is source folder exists
					{
						if (!Directory.Exists(Path.GetDirectoryName(folderDestName + "\\" + file))) // If destination folder doesn't exists - create it
						{
							Directory.CreateDirectory(Path.GetDirectoryName(folderDestName + "\\" + file));
						}
						try
						{
							File.Copy(folderSourceName + "\\" + file, folderDestName + "\\" + file, true); // Copy .mdl
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".dx80.vtx"), folderDestName + "\\" + file.Replace(".mdl", ".dx7_2bone.vtx"), true); // Copy .dx7_2bone.vtx
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".dx80.vtx"), folderDestName + "\\" + file.Replace(".mdl", ".dx80.vtx"), true); // Copy .dx80.vtx
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".dx90.vtx"), folderDestName + "\\" + file.Replace(".mdl", ".dx90.vtx"), true); // Copy .dx90.vtx
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".phy"), folderDestName + "\\" + file.Replace(".mdl", ".phy"), true); // Copy .phy
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".vvd"), folderDestName + "\\" + file.Replace(".mdl", ".vvd"), true); // Copy .vvd
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }
						try
						{
							File.Copy(folderSourceName + "\\" + file.Replace(".mdl", ".jpg"), folderDestName + "\\" + file.Replace(".mdl", ".jpg"), true); // Copy .jpg preview
						}
						catch (Exception ex) { Console.WriteLine("Cannot copy one of model files\r\n\t" + ex.Message); }

					}
				}
				else if (asset.Type == AssetType.Sound) // Sound
				{
					// Copy .wav/.mp3
					if (Directory.Exists(Path.GetDirectoryName(folderSourceName + "\\" + file))) // Is source folder exists
					{
						if (!Directory.Exists(Path.GetDirectoryName(folderDestName + "\\" + file))) // If destination folder doesn't exists - create it
						{
							Directory.CreateDirectory(Path.GetDirectoryName(folderDestName + "\\" + file));
						}
						try
						{
							File.Copy(folderSourceName + "\\" + file, folderDestName + "\\" + file, true); // Copy .wav/.mp3
						}
						catch (Exception ex) { Console.WriteLine(ex.Message); }
					}
				}
				else
				{

				}

				processedItemCount++;
				//	Console.WriteLine("{0}, {1}, {2} {3}", processedItemCount, allAssets.Count, (float)processedItemCount / allAssets.Count, (float)processedItemCount / allAssets.Count * 100);
				//	Console.WriteLine((int)Math.Round((float)processedItemCount / allAssets.Count * 100));
				backgroundWorker1.ReportProgress((int)Math.Round((float)processedItemCount / allAssets.Count * 100)); // What the fuck?
			}

			if (processedItemCount <= 0)
			{
				MessageBox.Show("Ни один файл не был скопирован. Возможно, все используемые на карте ассеты уже существуют в игре по-умолчанию,\r\nлибо папка-источник пуста.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			
		}
		
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			string[] args = (string[])e.Argument;
			string folderSourceName = args[0];
			string folderDestName = args[1];

			WorkerRoutine(folderSourceName, folderDestName);
			if (cbCheckHL2Folder.Checked) // Делать проверку ещё и в основной папке движка Source - hl2
			{
			/* WTF?
				string hl2folder = "";
				hl2folder = folderSourceName.Substring(folderSourceName.LastIndexOf('\\') + 1);
				if (hl2folder == "hl2") // Если выбранная папка-источник уже и так hl2 - не нужно ничего делать в этом случае
					return;

				Console.WriteLine("HL2Folder: " + hl2folder);
				WorkerRoutine(hl2folder, folderDestName);
			*/
				if (folderSourceName.EndsWith("hl2")) // Если выбранная папка-источник уже и так hl2 - не нужно ничего делать в этом случае
					return;

				string rootfolder = folderSourceName.Substring(0, folderSourceName.LastIndexOf('\\') + 1);
				Console.WriteLine("rootfolder: " + rootfolder);
			}
		}
		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			int currentValue = e.ProgressPercentage;

			// Hacky way to remove dat animation effect progressbar has
			toolStripProgressBar1.Value = currentValue;
			if (currentValue > 0)
				toolStripProgressBar1.Value = currentValue - 1;
			toolStripProgressBar1.Value = currentValue;
		//	Console.WriteLine(e.ProgressPercentage);
		}
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			DisableAllControls(false);
			statusStrip1.Items["toolStripStatusLabel1"].Text = "Готово";
			toolStripProgressBar1.Visible = false;
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			frmAbout about = new frmAbout();
			about.ShowDialog();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			Process.Start(tbDestFolder.Text);
		}

		private void btnGenerateList_Click(object sender, EventArgs e)
		{
			string path = tbDestFolder.Text + "\\filelist.txt";
			string contents = "";

		//	allAssets.ForEach((asset) => Console.WriteLine("Test: {0}", asset.Content));

			toolStripProgressBar1.Visible = true;

			string matlist = "Текстуры:\r\n";
			string mdllist = "\r\nМодели:\r\n";
			string sndlist = "\r\nЗвуки:\r\n";
			int i = 0;
			foreach (Asset asset in allAssets)
			{
				i++;
				toolStripProgressBar1.Value = 100 * i / allAssets.Count;

				if (asset.Type == AssetType.Material)
				{
					matlist += asset.Content + "\r\n";

					// Adding material parsing
					List<string> mats = new List<string>();
					LoadMaterialFile(g_folderSourceName + "\\" + asset.Content, out mats);
					if (mats.Count <= 0)
						continue;

					foreach (string mat in mats)
					{
						string source = "materials\\" + mat.Replace("/", "\\") + ".vtf";
						matlist += source + "\r\n";
					}
				}
				else if (asset.Type == AssetType.Model)
					mdllist += asset.Content + "\r\n";
				else if (asset.Type == AssetType.Sound)
					sndlist += asset.Content + "\r\n";
			}

			contents = matlist + mdllist + sndlist; // Собираем всё в кучу

			File.WriteAllText(path, contents);

			toolStripProgressBar1.Visible = false;
			toolStripProgressBar1.Value = 0;

			Msg("Список использованных файлов filelist.txt сохранён в папке " + g_folderDestName, true);
		}
	}
}
