using UnityEngine;

namespace NiTools{
using ByteGlyphs = RasterText.GlyphData<byte>;

public static partial class RasterText{
	//https://en.wikipedia.org/wiki/Code_page_866
	public static readonly ByteGlyphs cp866_8x8 = new ByteGlyphs(8, 8,
		"☒☺☻♥♦♣♠•◘○◙♂♀♪♫☼" +
		"►◄↕‼¶§▬↨↑↓→←∟↔▲▼" +
		" !\"#$%&'()*+,-./" +
		"0123456789:;<=>?" +
		"@ABCDEFGHIJKLMNO" +
		"PQRSTUVWXYZ[\\]^_" +
		"`abcdefghijklmno" +
		"pqrstuvwxyz{|}~⌂" +
		"АБВГДЕЖЗИЙКЛМНОП" +
		"РСТУФХЦЧШЩЪЫЬЭЮЯ" +
		"абвгдежзийклмноп" +
		"░▒▓│┤╡╢╖╕╣║╗╝╜╛┐" +
		"└┴┬├─┼╞╟╚╔╩╦╠═╬╧" +
		"╨╤╥╙╘╒╓╫╪┘┌█▄▌▐▀" +
		"рстуфхцчшщъыьэюя" +
		"ЁёЄєЇїЎў°∙·√№¤■",
		new byte[]{
			0xFF, 0xC3, 0xA5, 0x99, 0x99, 0xA5, 0xC3, 0xFF,
			0x00, 0x7E, 0x42, 0x66, 0x42, 0x5A, 0x7E, 0x00,
			0x00, 0x7E, 0x5A, 0x7E, 0x5A, 0x66, 0x7E, 0x00,
			0x00, 0x00, 0x36, 0x36, 0x3E, 0x1C, 0x08, 0x00,
			0x00, 0x08, 0x1C, 0x3E, 0x7F, 0x3E, 0x1C, 0x08,
			0x1C, 0x1C, 0x08, 0x6B, 0x7F, 0x6B, 0x08, 0x1C,
			0x08, 0x08, 0x1C, 0x3E, 0x3E, 0x1C, 0x3E, 0x00,
			0x00, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x00,
			0x00, 0x7E, 0x7E, 0x66, 0x66, 0x7E, 0x7E, 0x00,
			0x00, 0x18, 0x24, 0x42, 0x42, 0x24, 0x18, 0x00,
			0xFF, 0x81, 0xBD, 0xA5, 0xA5, 0xBD, 0x81, 0xFF,
			0x00, 0x0E, 0x06, 0x3A, 0x48, 0x48, 0x30, 0x00,
			0x00, 0x1C, 0x14, 0x1C, 0x08, 0x1C, 0x08, 0x00,
			0x00, 0x1E, 0x12, 0x1E, 0x10, 0x70, 0x60, 0x00,
			0x00, 0x3E, 0x22, 0x3E, 0x22, 0x66, 0x66, 0x00,
			0x99, 0x5A, 0x24, 0xC3, 0xC3, 0x24, 0x5A, 0x99,
			0x00, 0x60, 0x78, 0x7E, 0x7E, 0x78, 0x60, 0x00,
			0x00, 0x06, 0x1E, 0x7E, 0x7E, 0x1E, 0x06, 0x00,
			0x00, 0x18, 0x3C, 0x18, 0x18, 0x3C, 0x18, 0x00,
			0x00, 0x24, 0x24, 0x24, 0x24, 0x00, 0x24, 0x00,
			0x00, 0x3E, 0x52, 0x52, 0x32, 0x12, 0x12, 0x00,
			0x00, 0x3C, 0x42, 0x58, 0x24, 0x1A, 0x42, 0x3C,
			0x00, 0x00, 0x00, 0x00, 0x7E, 0x7E, 0x7E, 0x00,
			0x00, 0x18, 0x3C, 0x18, 0x3C, 0x18, 0x7E, 0x00,
			0x00, 0x18, 0x3C, 0x7E, 0x18, 0x18, 0x18, 0x00,
			0x00, 0x18, 0x18, 0x18, 0x7E, 0x3C, 0x18, 0x00,
			0x00, 0x08, 0x04, 0x7E, 0x7E, 0x04, 0x08, 0x00,
			0x00, 0x10, 0x20, 0x7E, 0x7E, 0x20, 0x10, 0x00,
			0x00, 0x00, 0x00, 0x40, 0x40, 0x40, 0x7E, 0x00,
			0x00, 0x00, 0x24, 0x42, 0x7E, 0x42, 0x24, 0x00,
			0x00, 0x18, 0x18, 0x3C, 0x3C, 0x7E, 0x7E, 0x00,
			0x00, 0x7E, 0x7E, 0x3C, 0x3C, 0x18, 0x18, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x18, 0x18, 0x18, 0x18, 0x00, 0x18, 0x00,
			0x00, 0x24, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x24, 0x7E, 0x24, 0x24, 0x7E, 0x24, 0x00,
			0x00, 0x18, 0x3E, 0x40, 0x3C, 0x02, 0x7C, 0x18,
			0x00, 0x62, 0x64, 0x08, 0x10, 0x26, 0x46, 0x00,
			0x00, 0x18, 0x28, 0x30, 0x4A, 0x44, 0x3A, 0x00,
			0x00, 0x08, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x00,
			0x00, 0x10, 0x08, 0x08, 0x08, 0x08, 0x10, 0x00,
			0x00, 0x00, 0x24, 0x18, 0x3C, 0x18, 0x24, 0x00,
			0x00, 0x00, 0x08, 0x08, 0x3E, 0x08, 0x08, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x08,
			0x00, 0x00, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00,
			0x00, 0x04, 0x08, 0x08, 0x10, 0x10, 0x20, 0x00,
			0x00, 0x3C, 0x46, 0x4A, 0x52, 0x62, 0x3C, 0x00,
			0x00, 0x08, 0x18, 0x08, 0x08, 0x08, 0x1C, 0x00,
			0x00, 0x3C, 0x42, 0x02, 0x3C, 0x40, 0x7E, 0x00,
			0x00, 0x3C, 0x02, 0x1C, 0x02, 0x02, 0x3C, 0x00,
			0x00, 0x42, 0x42, 0x7E, 0x02, 0x02, 0x02, 0x00,
			0x00, 0x7E, 0x40, 0x7C, 0x02, 0x02, 0x7C, 0x00,
			0x00, 0x3C, 0x40, 0x7C, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x7E, 0x02, 0x04, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x3C, 0x42, 0x3C, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x3E, 0x02, 0x3C, 0x00,
			0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x08, 0x00,
			0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x08, 0x08,
			0x00, 0x06, 0x18, 0x60, 0x60, 0x18, 0x06, 0x00,
			0x00, 0x00, 0x3C, 0x00, 0x00, 0x3C, 0x00, 0x00,
			0x00, 0x60, 0x18, 0x06, 0x06, 0x18, 0x60, 0x00,
			0x00, 0x1C, 0x22, 0x04, 0x08, 0x00, 0x08, 0x00,
			0x00, 0x3C, 0x42, 0x5A, 0x5C, 0x40, 0x3E, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x00,
			0x00, 0x7C, 0x42, 0x7C, 0x42, 0x42, 0x7C, 0x00,
			0x00, 0x3C, 0x42, 0x40, 0x40, 0x42, 0x3C, 0x00,
			0x00, 0x7C, 0x42, 0x42, 0x42, 0x42, 0x7C, 0x00,
			0x00, 0x7E, 0x40, 0x78, 0x40, 0x40, 0x7E, 0x00,
			0x00, 0x7E, 0x40, 0x78, 0x40, 0x40, 0x40, 0x00,
			0x00, 0x3C, 0x42, 0x40, 0x4E, 0x42, 0x3C, 0x00,
			0x00, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x1C, 0x08, 0x08, 0x08, 0x08, 0x1C, 0x00,
			0x00, 0x1C, 0x08, 0x08, 0x08, 0x28, 0x10, 0x00,
			0x00, 0x42, 0x4C, 0x70, 0x48, 0x44, 0x42, 0x00,
			0x00, 0x40, 0x40, 0x40, 0x40, 0x40, 0x7E, 0x00,
			0x00, 0x42, 0x66, 0x5A, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x42, 0x62, 0x52, 0x4A, 0x46, 0x42, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x7C, 0x42, 0x7C, 0x40, 0x40, 0x40, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x4A, 0x44, 0x3A, 0x00,
			0x00, 0x7C, 0x42, 0x7C, 0x48, 0x44, 0x42, 0x00,
			0x00, 0x3C, 0x42, 0x30, 0x0C, 0x42, 0x3C, 0x00,
			0x00, 0x3E, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x42, 0x42, 0x42, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x42, 0x42, 0x42, 0x42, 0x24, 0x18, 0x00,
			0x00, 0x4A, 0x4A, 0x4A, 0x4A, 0x4A, 0x34, 0x00,
			0x00, 0x42, 0x24, 0x18, 0x18, 0x24, 0x42, 0x00,
			0x00, 0x42, 0x24, 0x18, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x7E, 0x04, 0x08, 0x10, 0x20, 0x7E, 0x00,
			0x00, 0x18, 0x10, 0x10, 0x10, 0x10, 0x18, 0x00,
			0x00, 0x20, 0x10, 0x10, 0x08, 0x08, 0x04, 0x00,
			0x00, 0x18, 0x08, 0x08, 0x08, 0x08, 0x18, 0x00,
			0x00, 0x18, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7E, 0x00,
			0x00, 0x10, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x38, 0x04, 0x3C, 0x44, 0x3A, 0x00,
			0x00, 0x40, 0x40, 0x40, 0x7C, 0x42, 0x7C, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x40, 0x42, 0x3C, 0x00,
			0x00, 0x02, 0x02, 0x02, 0x3E, 0x42, 0x3E, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x7E, 0x40, 0x3C, 0x00,
			0x00, 0x08, 0x10, 0x38, 0x10, 0x10, 0x10, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x3E, 0x02, 0x3C, 0x00,
			0x00, 0x40, 0x40, 0x40, 0x7C, 0x42, 0x42, 0x00,
			0x00, 0x00, 0x08, 0x00, 0x08, 0x08, 0x1C, 0x00,
			0x00, 0x00, 0x08, 0x00, 0x08, 0x08, 0x28, 0x10,
			0x00, 0x40, 0x44, 0x48, 0x70, 0x48, 0x44, 0x00,
			0x00, 0x10, 0x10, 0x10, 0x10, 0x10, 0x18, 0x00,
			0x00, 0x00, 0x74, 0x4A, 0x4A, 0x4A, 0x4A, 0x00,
			0x00, 0x00, 0x7C, 0x42, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x7C, 0x42, 0x42, 0x42, 0x7C, 0x40,
			0x00, 0x00, 0x3A, 0x46, 0x42, 0x46, 0x3A, 0x02,
			0x00, 0x00, 0x5C, 0x62, 0x40, 0x40, 0x40, 0x00,
			0x00, 0x00, 0x3E, 0x40, 0x3C, 0x02, 0x7C, 0x00,
			0x00, 0x10, 0x38, 0x10, 0x10, 0x14, 0x08, 0x00,
			0x00, 0x00, 0x42, 0x42, 0x42, 0x46, 0x3A, 0x00,
			0x00, 0x00, 0x42, 0x42, 0x24, 0x24, 0x18, 0x00,
			0x00, 0x00, 0x4A, 0x4A, 0x4A, 0x4A, 0x34, 0x00,
			0x00, 0x00, 0x42, 0x24, 0x18, 0x24, 0x42, 0x00,
			0x00, 0x00, 0x42, 0x42, 0x42, 0x3E, 0x02, 0x3C,
			0x00, 0x00, 0x7E, 0x04, 0x18, 0x20, 0x7E, 0x00,
			0x00, 0x18, 0x10, 0x20, 0x10, 0x10, 0x18, 0x00,
			0x00, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x18, 0x08, 0x04, 0x08, 0x08, 0x18, 0x00,
			0x00, 0x00, 0x20, 0x5A, 0x04, 0x00, 0x00, 0x00,
			0x00, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x00,
			0x00, 0x7E, 0x40, 0x7C, 0x42, 0x42, 0x7C, 0x00,
			0x00, 0x7C, 0x42, 0x7C, 0x42, 0x42, 0x7C, 0x00,
			0x00, 0x7E, 0x40, 0x40, 0x40, 0x40, 0x40, 0x00,
			0x00, 0x1C, 0x14, 0x14, 0x24, 0x24, 0x7E, 0x42,
			0x00, 0x7E, 0x40, 0x78, 0x40, 0x40, 0x7E, 0x00,
			0x00, 0x2A, 0x2A, 0x1C, 0x1C, 0x2A, 0x2A, 0x00,
			0x00, 0x7C, 0x02, 0x3C, 0x02, 0x42, 0x3C, 0x00,
			0x00, 0x42, 0x46, 0x4A, 0x52, 0x62, 0x42, 0x00,
			0x18, 0x42, 0x46, 0x4A, 0x52, 0x62, 0x42, 0x00,
			0x00, 0x46, 0x48, 0x70, 0x48, 0x44, 0x42, 0x00,
			0x00, 0x1E, 0x12, 0x12, 0x12, 0x52, 0x22, 0x00,
			0x00, 0x42, 0x66, 0x5A, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x3C, 0x42, 0x42, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x00,
			0x00, 0x7C, 0x42, 0x42, 0x7C, 0x40, 0x40, 0x00,
			0x00, 0x3C, 0x42, 0x40, 0x40, 0x42, 0x3C, 0x00,
			0x00, 0x3E, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x42, 0x24, 0x18, 0x10, 0x20, 0x40, 0x00,
			0x00, 0x08, 0x1C, 0x2A, 0x2A, 0x1C, 0x08, 0x00,
			0x00, 0x42, 0x24, 0x18, 0x18, 0x24, 0x42, 0x00,
			0x00, 0x44, 0x44, 0x44, 0x44, 0x44, 0x7E, 0x02,
			0x00, 0x42, 0x42, 0x42, 0x3E, 0x02, 0x02, 0x00,
			0x00, 0x4A, 0x4A, 0x4A, 0x4A, 0x4A, 0x7E, 0x00,
			0x00, 0x4A, 0x4A, 0x4A, 0x4A, 0x4A, 0x7F, 0x01,
			0x00, 0x60, 0x20, 0x3C, 0x22, 0x22, 0x3C, 0x00,
			0x00, 0x42, 0x42, 0x72, 0x4A, 0x4A, 0x72, 0x00,
			0x00, 0x40, 0x40, 0x7C, 0x42, 0x42, 0x7C, 0x00,
			0x00, 0x3C, 0x42, 0x1E, 0x02, 0x42, 0x3C, 0x00,
			0x00, 0x4C, 0x52, 0x52, 0x72, 0x52, 0x4C, 0x00,
			0x00, 0x3E, 0x42, 0x42, 0x3E, 0x12, 0x62, 0x00,
			0x00, 0x00, 0x38, 0x04, 0x3C, 0x44, 0x3A, 0x00,
			0x00, 0x02, 0x3C, 0x40, 0x7C, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x7C, 0x42, 0x7C, 0x42, 0x7C, 0x00,
			0x00, 0x00, 0x7E, 0x40, 0x40, 0x40, 0x40, 0x00,
			0x00, 0x00, 0x1C, 0x14, 0x24, 0x24, 0x7E, 0x42,
			0x00, 0x00, 0x3C, 0x42, 0x7E, 0x40, 0x3C, 0x00,
			0x00, 0x00, 0x2A, 0x2A, 0x1C, 0x2A, 0x2A, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x1C, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x42, 0x46, 0x5A, 0x62, 0x42, 0x00,
			0x00, 0x18, 0x42, 0x46, 0x5A, 0x62, 0x42, 0x00,
			0x00, 0x00, 0x42, 0x44, 0x78, 0x44, 0x42, 0x00,
			0x00, 0x00, 0x1E, 0x12, 0x12, 0x52, 0x22, 0x00,
			0x00, 0x00, 0x42, 0x66, 0x5A, 0x42, 0x42, 0x00,
			0x00, 0x00, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x42, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x00,
			0xAA, 0x00, 0xAA, 0x00, 0xAA, 0x00, 0xAA, 0x00,
			0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x55,
			0xFF, 0xAA, 0xFF, 0xAA, 0xFF, 0xAA, 0xFF, 0xAA,
			0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08,
			0x08, 0x08, 0x08, 0x08, 0xF8, 0x08, 0x08, 0x08,
			0x08, 0x08, 0xF8, 0x08, 0x08, 0xF8, 0x08, 0x08,
			0x24, 0x24, 0x24, 0x24, 0xE4, 0x24, 0x24, 0x24,
			0x00, 0x00, 0x00, 0x00, 0xFC, 0x24, 0x24, 0x24,
			0x00, 0x00, 0xF8, 0x08, 0x08, 0xF8, 0x08, 0x08,
			0x24, 0x24, 0xE4, 0x04, 0x04, 0xE4, 0x24, 0x24,
			0x24, 0x24, 0x24, 0x24, 0x24, 0x24, 0x24, 0x24,
			0x00, 0x00, 0xFC, 0x04, 0x04, 0xE4, 0x24, 0x24,
			0x24, 0x24, 0xE4, 0x04, 0x04, 0xFC, 0x00, 0x00,
			0x24, 0x24, 0x24, 0x24, 0xFC, 0x00, 0x00, 0x00,
			0x08, 0x08, 0xF8, 0x08, 0x08, 0xF8, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0xF8, 0x08, 0x08, 0x08,
			0x08, 0x08, 0x08, 0x08, 0x0F, 0x00, 0x00, 0x00,
			0x08, 0x08, 0x08, 0x08, 0xFF, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0xFF, 0x08, 0x08, 0x08,
			0x08, 0x08, 0x08, 0x08, 0x0F, 0x08, 0x08, 0x08,
			0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00,
			0x08, 0x08, 0x08, 0x08, 0xFF, 0x08, 0x08, 0x08,
			0x08, 0x08, 0x0F, 0x08, 0x08, 0x0F, 0x08, 0x08,
			0x24, 0x24, 0x24, 0x24, 0x27, 0x24, 0x24, 0x24,
			0x24, 0x24, 0x27, 0x20, 0x20, 0x3F, 0x00, 0x00,
			0x00, 0x00, 0x3F, 0x20, 0x20, 0x27, 0x24, 0x24,
			0x24, 0x24, 0xE7, 0x00, 0x00, 0xFF, 0x00, 0x00,
			0x00, 0x00, 0xFF, 0x00, 0x00, 0xE7, 0x24, 0x24,
			0x24, 0x24, 0x27, 0x20, 0x20, 0x27, 0x24, 0x24,
			0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00,
			0x24, 0x24, 0xE7, 0x00, 0x00, 0xE7, 0x24, 0x24,
			0x08, 0x08, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00,
			0x24, 0x24, 0x24, 0x24, 0xFF, 0x00, 0x00, 0x00,
			0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0x08, 0x08,
			0x00, 0x00, 0x00, 0x00, 0xFF, 0x24, 0x24, 0x24,
			0x24, 0x24, 0x24, 0x24, 0x3F, 0x00, 0x00, 0x00,
			0x08, 0x08, 0x0F, 0x08, 0x08, 0x0F, 0x00, 0x00,
			0x00, 0x00, 0x0F, 0x08, 0x08, 0x0F, 0x08, 0x08,
			0x00, 0x00, 0x00, 0x00, 0x3F, 0x24, 0x24, 0x24,
			0x24, 0x24, 0x24, 0x24, 0xFF, 0x24, 0x24, 0x24,
			0x08, 0x08, 0xFF, 0x08, 0x08, 0xFF, 0x08, 0x08,
			0x08, 0x08, 0x08, 0x08, 0xF8, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x0F, 0x08, 0x08, 0x08,
			0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
			0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF,
			0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0,
			0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F,
			0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x7C, 0x42, 0x42, 0x7C, 0x40, 0x40,
			0x00, 0x00, 0x3C, 0x42, 0x40, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x3E, 0x08, 0x08, 0x08, 0x08, 0x00,
			0x00, 0x00, 0x42, 0x24, 0x18, 0x10, 0x60, 0x00,
			0x00, 0x00, 0x1C, 0x2A, 0x2A, 0x1C, 0x08, 0x00,
			0x00, 0x00, 0x42, 0x24, 0x18, 0x24, 0x42, 0x00,
			0x00, 0x00, 0x44, 0x44, 0x44, 0x44, 0x7E, 0x02,
			0x00, 0x00, 0x42, 0x42, 0x3E, 0x02, 0x02, 0x00,
			0x00, 0x00, 0x4A, 0x4A, 0x4A, 0x4A, 0x7E, 0x00,
			0x00, 0x00, 0x54, 0x54, 0x54, 0x54, 0x7E, 0x02,
			0x00, 0x00, 0x60, 0x20, 0x3C, 0x22, 0x3C, 0x00,
			0x00, 0x00, 0x42, 0x42, 0x72, 0x4A, 0x72, 0x00,
			0x00, 0x00, 0x40, 0x40, 0x7C, 0x42, 0x7C, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x0E, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x4C, 0x52, 0x72, 0x52, 0x4C, 0x00,
			0x00, 0x00, 0x3E, 0x42, 0x3E, 0x12, 0x62, 0x00,
			0x24, 0x00, 0x7E, 0x40, 0x78, 0x40, 0x7E, 0x00,
			0x24, 0x00, 0x3C, 0x42, 0x7E, 0x40, 0x3C, 0x00,
			0x00, 0x3C, 0x42, 0x70, 0x40, 0x42, 0x3C, 0x00,
			0x00, 0x00, 0x3C, 0x42, 0x70, 0x42, 0x3C, 0x00,
			0x14, 0x00, 0x1C, 0x08, 0x08, 0x08, 0x1C, 0x00,
			0x00, 0x14, 0x00, 0x08, 0x08, 0x08, 0x1C, 0x00,
			0x24, 0x18, 0x42, 0x24, 0x18, 0x10, 0x60, 0x00,
			0x00, 0x24, 0x18, 0x42, 0x24, 0x18, 0x10, 0x60,
			0x00, 0x18, 0x24, 0x18, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x08, 0x1C, 0x08, 0x00, 0x00,
			0x00, 0x00, 0x18, 0x3C, 0x3C, 0x18, 0x00, 0x00,
			0x00, 0x06, 0x04, 0x04, 0x68, 0x18, 0x08, 0x00,
			0x00, 0x4A, 0x6A, 0x68, 0x5A, 0x58, 0x48, 0x00,
			0x00, 0x5A, 0x24, 0x42, 0x42, 0x24, 0x5A, 0x00,
			0x00, 0x00, 0x7C, 0x7C, 0x7C, 0x7C, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x42, 0x7E, 0x00
		}
	);
}

}