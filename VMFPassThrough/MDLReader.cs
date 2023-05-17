using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace VMFPassThrough
{
	class MDLReader
	{
		private void ReadMdlHeader00(BinaryReader theInputFileReader, out int version)
		{
			// ReadMdlHeader00
			//	long fileOffsetStart = theInputFileReader.BaseStream.Position;
			//	long fileOffsetEnd = 0;
			// Offsets: 0x00, 0x04, 0x08, 0x0C (12), 0x4C (76)
			/*Me.theMdlFileData.id =*/
			theInputFileReader.ReadChars(4);
			//Me.theMdlFileData.theID = Me.theMdlFileData.id;
			/*Me.theMdlFileData.version =*/
			version = 
			theInputFileReader.ReadInt32();

			/*Me.theMdlFileData.checksum =*/
			theInputFileReader.ReadInt32();

			/*Me.theMdlFileData.name =*/
			theInputFileReader.ReadChars(64);
			//Me.theMdlFileData.theName = CStr(Me.theMdlFileData.name).Trim(Chr(0));

			/*Me.theMdlFileData.fileSize =*/
			theInputFileReader.ReadInt32();
			//Me.theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length; // Need?

			//	fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			//	If logDescription <> "" Then
			//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription)
			//	End If
		}
		private void ReadMdlHeader01_48(BinaryReader theInputFileReader, out List<string> folders, out List<string> files)
		{
			// ReadMdlHeader01
			// Offsets: 0x50, 0x54, 0x58
			/*Me.theMdlFileData.eyePositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			/*Me.theMdlFileData.illuminationPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			/*Me.theMdlFileData.hullMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			/*Me.theMdlFileData.hullMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			/*Me.theMdlFileData.viewBoundingBoxMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x98
			/*Me.theMdlFileData.flags =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			/*Me.theMdlFileData.boneCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			/*Me.theMdlFileData.boneControllerCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneControllerOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			/*Me.theMdlFileData.hitboxSetCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.hitboxSetOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xB4 (180), 0xB8
			/*Me.theMdlFileData.localAnimationCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.localAnimationOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xBC (188), 0xC0 (192)
			/*Me.theMdlFileData.localSequenceCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.localSequenceOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xC4, 0xC8
			/*Me.theMdlFileData.activityListVersion =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.eventsIndexed =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xCC (204), 0xD0 (208)
			/*Me.theMdlFileData.textureCount =*/
			int textureCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.textureOffset =*/
			int textureOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xD4 (212), 0xD8
			/*Me.theMdlFileData.texturePathCount =*/
			int texturePathCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.texturePathOffset =*/
			int texturePathOffsetO = theInputFileReader.ReadInt32();

			// Reading texture paths
			folders = new List<string>(texturePathCount);
			if (texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				theInputFileReader.BaseStream.Seek(texturePathOffsetO, SeekOrigin.Begin);
				//	List<string> theTexturePaths = new List<string>(texturePathCount);

				int texturePathOffset = 0;
				for (int i = 0; i < texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					string aTexturePath = "";
					texturePathOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexturePath = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = /*FileManager.*/GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					}
					//	theTexturePaths.Add(aTexturePath);
					folders.Add(aTexturePath);
					//	Console.WriteLine(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			// Read textures
			files = new List<string>(textureCount);
			if (textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;

				theInputFileReader.BaseStream.Seek(textureOffset, SeekOrigin.Begin);

				//	List<string> theTextures = new List<string>(textureCount);

				for (int i = 0; i < textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					//	Dim aTexture As New SourceMdlTexture();
					/*aTexture.nameOffset =*/
					int nameOffset = theInputFileReader.ReadInt32();
					/*aTexture.flags =*/
					theInputFileReader.ReadInt32();
					/*aTexture.used =*/
					theInputFileReader.ReadInt32();
					/*aTexture.unused1 =*/
					theInputFileReader.ReadInt32();
					/*aTexture.materialP =*/
					theInputFileReader.ReadInt32();
					/*aTexture.clientMaterialP =*/
					theInputFileReader.ReadInt32();
					for (int x = 0; x < 9; x++)
					{
						/*aTexture.unused(x) =*/
						theInputFileReader.ReadInt32();
					}
					//	theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					string thePathFileName = "";
					if (nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + nameOffset, SeekOrigin.Begin);

						thePathFileName = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);
						thePathFileName = /*FileManager.*/GetNormalizedPathFileName(thePathFileName);

						//	theTextures.Add(thePathFileName);
						files.Add(thePathFileName);
						//	Console.WriteLine(thePathFileName + ".vmt/.vtf");
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
		}
		private void ReadMdlHeader01_37(BinaryReader theInputFileReader, out List<string> folders, out List<string> files)
		{
			// ReadMdlHeader01
			// Offsets: 0x50, 0x54, 0x58
			/*Me.theMdlFileData.eyePositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			/*Me.theMdlFileData.illuminationPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			/*Me.theMdlFileData.hullMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			/*Me.theMdlFileData.hullMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			/*Me.theMdlFileData.viewBoundingBoxMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x98
			/*Me.theMdlFileData.flags =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			/*Me.theMdlFileData.boneCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			/*Me.theMdlFileData.boneControllerCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneControllerOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			/*Me.theMdlFileData.hitboxSetCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.hitboxSetOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.animationCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.animationOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.animationGroupCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.animationGroupOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.boneDescCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneDescOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.localSequenceCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.localSequenceOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.sequencesIndexedFlag =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.textureCount =*/
			int textureCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.textureOffset =*/
			int textureOffset = theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.texturePathCount =*/
			int texturePathCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.texturePathOffset =*/
			int texturePathOffsetO = theInputFileReader.ReadInt32();

			// Reading texture paths
			folders = new List<string>(texturePathCount);
			if (texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				theInputFileReader.BaseStream.Seek(texturePathOffsetO, SeekOrigin.Begin);
				//	List<string> theTexturePaths = new List<string>(texturePathCount);

				int texturePathOffset = 0;
				for (int i = 0; i < texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					string aTexturePath = "";
					texturePathOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexturePath = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = /*FileManager.*/GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					}
					//	theTexturePaths.Add(aTexturePath);
					folders.Add(aTexturePath);
					//	Console.WriteLine(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			// Read textures
			files = new List<string>(textureCount);
			if (textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;

				theInputFileReader.BaseStream.Seek(textureOffset, SeekOrigin.Begin);

				//	List<string> theTextures = new List<string>(textureCount);

				for (int i = 0; i < textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					//	Dim aTexture As New SourceMdlTexture();
					/*aTexture.nameOffset =*/
					int nameOffset = theInputFileReader.ReadInt32();
					/*aTexture.flags =*/
					theInputFileReader.ReadInt32();
					/*aTexture.used =*/
					theInputFileReader.ReadInt32();
					/*aTexture.unused1 =*/
					theInputFileReader.ReadInt32();
					/*aTexture.materialP =*/
					theInputFileReader.ReadInt32();
					/*aTexture.clientMaterialP =*/
					theInputFileReader.ReadInt32();
					for (int x = 0; x < 9; x++)
					{
						/*aTexture.unused(x) =*/
						theInputFileReader.ReadInt32();
					}
					//	theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					string thePathFileName = "";
					if (nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + nameOffset, SeekOrigin.Begin);

						thePathFileName = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);
						thePathFileName = /*FileManager.*/GetNormalizedPathFileName(thePathFileName);

						//	theTextures.Add(thePathFileName);
						files.Add(thePathFileName);
						//	Console.WriteLine(thePathFileName + ".vmt/.vtf");
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
		}
		private void ReadMdlHeader01_36(BinaryReader theInputFileReader, out List<string> folders, out List<string> files)
		{
			// ReadMdlHeader01
			// Offsets: 0x50, 0x54, 0x58
			/*Me.theMdlFileData.eyePositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			/*Me.theMdlFileData.illuminationPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			/*Me.theMdlFileData.hullMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			/*Me.theMdlFileData.hullMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			/*Me.theMdlFileData.viewBoundingBoxMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x98
			/*Me.theMdlFileData.flags =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			/*Me.theMdlFileData.boneCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			/*Me.theMdlFileData.boneControllerCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneControllerOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			/*Me.theMdlFileData.hitboxSetCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.hitboxSetOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.animationCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.animationOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.localSequenceCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.localSequenceOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.sequencesIndexedFlag =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.textureCount =*/
			int textureCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.textureOffset =*/
			int textureOffset = theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.texturePathCount =*/
			int texturePathCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.texturePathOffset =*/
			int texturePathOffsetO = theInputFileReader.ReadInt32();

			// Reading texture paths
			folders = new List<string>(texturePathCount);
			if (texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				theInputFileReader.BaseStream.Seek(texturePathOffsetO, SeekOrigin.Begin);
				//	List<string> theTexturePaths = new List<string>(texturePathCount);

				int texturePathOffset = 0;
				for (int i = 0; i < texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					string aTexturePath = "";
					texturePathOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexturePath = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = /*FileManager.*/GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					}
					//	theTexturePaths.Add(aTexturePath);
					folders.Add(aTexturePath);
					//	Console.WriteLine(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			// Read textures
			files = new List<string>(textureCount);
			if (textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;

				theInputFileReader.BaseStream.Seek(textureOffset, SeekOrigin.Begin);

				//	List<string> theTextures = new List<string>(textureCount);

				for (int i = 0; i < textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					//	Dim aTexture As New SourceMdlTexture();
					/*aTexture.nameOffset =*/
					int nameOffset = theInputFileReader.ReadInt32();
					/*aTexture.flags =*/
					theInputFileReader.ReadInt32();
					/*aTexture.used =*/
					theInputFileReader.ReadInt32();
					/*aTexture.unused1 =*/
					theInputFileReader.ReadInt32();
					/*aTexture.materialP =*/
					theInputFileReader.ReadInt32();
					/*aTexture.clientMaterialP =*/
					theInputFileReader.ReadInt32();
					for (int x = 0; x < 9; x++)
					{
						/*aTexture.unused(x) =*/
						theInputFileReader.ReadInt32();
					}
					//	theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					string thePathFileName = "";
					if (nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + nameOffset, SeekOrigin.Begin);

						thePathFileName = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);
						thePathFileName = /*FileManager.*/GetNormalizedPathFileName(thePathFileName);

						//	theTextures.Add(thePathFileName);
						files.Add(thePathFileName);
						//	Console.WriteLine(thePathFileName + ".vmt/.vtf");
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
		}
		private void ReadMdlHeader01_31(BinaryReader theInputFileReader, out List<string> folders, out List<string> files)
		{
			// ReadMdlHeader01
			// Offsets: 0x50, 0x54, 0x58
			/*Me.theMdlFileData.eyePositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.eyePositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			/*Me.theMdlFileData.illuminationPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.illuminationPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			/*Me.theMdlFileData.hullMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			/*Me.theMdlFileData.hullMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.hullMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			/*Me.theMdlFileData.viewBoundingBoxMinPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMinPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionX =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionY =*/
			theInputFileReader.ReadSingle();
			/*Me.theMdlFileData.viewBoundingBoxMaxPositionZ =*/
			theInputFileReader.ReadSingle();

			// Offsets: 0x98
			/*Me.theMdlFileData.flags =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			/*Me.theMdlFileData.boneCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			/*Me.theMdlFileData.boneControllerCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.boneControllerOffset =*/
			theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			/*Me.theMdlFileData.hitboxSetCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.hitboxSetOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.animationCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.animationOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.localSequenceCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.localSequenceOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.sequencesIndexedFlag =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupCount =*/
			theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.sequenceGroupOffset =*/
			theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.textureCount =*/
			int textureCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.textureOffset =*/
			int textureOffset = theInputFileReader.ReadInt32();

			// 
			/*Me.theMdlFileData.texturePathCount =*/
			int texturePathCount = theInputFileReader.ReadInt32();
			/*Me.theMdlFileData.texturePathOffset =*/
			int texturePathOffsetO = theInputFileReader.ReadInt32();

			// Reading texture paths
			folders = new List<string>(texturePathCount);
			if (texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				theInputFileReader.BaseStream.Seek(texturePathOffsetO, SeekOrigin.Begin);
				//	List<string> theTexturePaths = new List<string>(texturePathCount);

				int texturePathOffset = 0;
				for (int i = 0; i < texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					string aTexturePath = "";
					texturePathOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexturePath = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = /*FileManager.*/GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					}
					//	theTexturePaths.Add(aTexturePath);
					folders.Add(aTexturePath);
					//	Console.WriteLine(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			// Read textures
			files = new List<string>(textureCount);
			if (textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;

				theInputFileReader.BaseStream.Seek(textureOffset, SeekOrigin.Begin);

				//	List<string> theTextures = new List<string>(textureCount);

				for (int i = 0; i < textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					//	Dim aTexture As New SourceMdlTexture();
					/*aTexture.nameOffset =*/
					int nameOffset = theInputFileReader.ReadInt32();
					/*aTexture.flags =*/
					theInputFileReader.ReadInt32();
					/*aTexture.used =*/
					theInputFileReader.ReadInt32();
					/*aTexture.unused1 =*/
					theInputFileReader.ReadInt32();
					/*aTexture.materialP =*/
					theInputFileReader.ReadInt32();
					/*aTexture.clientMaterialP =*/
					theInputFileReader.ReadInt32();
					for (int x = 0; x < 9; x++)
					{
						/*aTexture.unused(x) =*/
						theInputFileReader.ReadInt32();
					}
					//	theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					string thePathFileName = "";
					if (nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + nameOffset, SeekOrigin.Begin);

						thePathFileName = /*FileManager.*/ReadNullTerminatedString(theInputFileReader);
						thePathFileName = /*FileManager.*/GetNormalizedPathFileName(thePathFileName);

						//	theTextures.Add(thePathFileName);
						files.Add(thePathFileName);
						//	Console.WriteLine(thePathFileName + ".vmt/.vtf");
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
		}
		// ОЧЕНЬ урезанная версия ридера из Crowbar Decompiler (спасибо, няша)
		public void ReadTexturePaths(string mdlFile, out List<string> folders, out List<string> files, out int version)
		{
			folders = new List<string>();
			files = new List<string>();
			version = -1;
			try
			{
				// folderSourceName + "\\models\\dav0r\\hoverball.mdl"
				//	FileStream inputFileStream = new FileStream(folderSourceName + "\\models\\dav0r\\hoverball.mdl", FileMode.	Open, FileAccess.Read, FileShare.Read);
				FileStream inputFileStream = new FileStream(mdlFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				BinaryReader theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

				// Readings
				ReadMdlHeader00(theInputFileReader, out version);
				Console.WriteLine(String.Format("Reading header from {0} version model", version));
				switch (version)
				{
					default:
					case 53:
					case 49:
					case 48:
					case 46:
					case 45:
					case 44:
						ReadMdlHeader01_48(theInputFileReader, out folders, out files);
						break;
					case 37:
						ReadMdlHeader01_37(theInputFileReader, out folders, out files);
						break;
					case 36:
						ReadMdlHeader01_36(theInputFileReader, out folders, out files);
						break;
					case 31:
						ReadMdlHeader01_31(theInputFileReader, out folders, out files);
						break;
				}

				if (theInputFileReader != null)
					theInputFileReader.Close();
			}
			catch { }
		}
		// MDL Reader
		public string ReadNullTerminatedString(BinaryReader inputFileReader)
		{
			StringBuilder text = new StringBuilder();
			text.Length = 0;
			while (inputFileReader.PeekChar() > 0)
			{
				text.Append(inputFileReader.ReadChar());
			}
			// Read the null character.
			inputFileReader.ReadChar();
			return text.ToString();
		}
		public string GetNormalizedPathFileName(string givenPathFileName)
		{
			string cleanPathFileName = "";

			cleanPathFileName = givenPathFileName;
			if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
			{
				cleanPathFileName = givenPathFileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}

			return cleanPathFileName;
		}
	}
}
