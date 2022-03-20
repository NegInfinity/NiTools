/*
This file implements a vector based "typeface" which can be used with a line drawing delegate to produce wireframe text.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NiTools{

public static class VectorText{
	public delegate void LineDelegate2D(Vector2 a, Vector2 b);

	class LetterData{
		public float[] floats = null;
		public int[] ints = null;
		public Vector2 getVertex(int index, float scale){
			var offset = index * 2;
			return new Vector2(floats[offset] * scale, floats[offset + 1] * scale);
		}
		public Vector2 getVertex(int index){
			var offset = index * 2;
			return new Vector2(floats[offset], floats[offset + 1]);
		}
		public LetterData(float[] floats_, int[] ints_){
			floats = floats_;
			ints = ints_;
		}
	}

	/*
	This "typeset" was originally designed in blender then converted into 2d vector form via a python script.
	It is likely possible to compress it further, but there's not much point.
	*/
	static readonly Dictionary<char, LetterData> letters = new(){
		{' ', new LetterData(new float[]{}, new int[]{})},
		{'!', new LetterData(new float[]{0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.5f, 0.1f, 0.4f, 1.8f, 0.5f, 1.9f, 0.6f, 1.8f, 0.5f, 0.5f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4})},
		{'"', new LetterData(new float[]{0.3f, 1.7f, 0.4f, 1.8f, 0.5f, 1.7f, 0.3f, 1.5f, 0.5f, 1.7f, 0.6f, 1.8f, 0.7f, 1.7f, 0.5f, 1.5f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4})},
		{'#', new LetterData(new float[]{0.4f, 0.5f, 0.4f, 1.4f, 0.2f, 1.0f, 0.8f, 1.3f, 0.6f, 1.5f, 0.6f, 0.6f, 0.2f, 0.7f, 0.8f, 1.0f}, new int[]{0, 1, 2, 3, 4, 5, 6, 7})},
		{'%', new LetterData(new float[]{0.2f, 1.6f, 0.3f, 1.7f, 0.4f, 1.6f, 0.3f, 1.5f, 0.8f, 1.7f, 0.2f, 0.3f, 0.6f, 0.4f, 0.7f, 0.5f, 0.8f, 0.4f, 0.7f, 0.3f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 6, 7, 7, 8, 8, 9, 9, 6})},
		{'\'', new LetterData(new float[]{0.4f, 1.7f, 0.5f, 1.8f, 0.6f, 1.7f, 0.4f, 1.5f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0})},
		{'(', new LetterData(new float[]{0.4f, 0.3f, 0.4f, 1.7f, 0.6f, 1.9f, 0.6f, 0.1f}, new int[]{0, 1, 1, 2, 3, 0})},
		{')', new LetterData(new float[]{0.4f, 1.9f, 0.6f, 1.7f, 0.6f, 0.3f, 0.4f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3})},
		{'*', new LetterData(new float[]{0.6f, 0.8f, 0.4f, 1.2f, 0.6f, 1.2f, 0.4f, 0.8f, 0.7f, 1.0f, 0.3f, 1.0f}, new int[]{0, 1, 2, 3, 4, 5})},
		{'+', new LetterData(new float[]{0.2f, 1.0f, 0.8f, 1.0f, 0.5f, 1.3f, 0.5f, 0.7f}, new int[]{0, 1, 2, 3})},
		{',', new LetterData(new float[]{0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.4f, 0.0f}, new int[]{0, 1, 1, 2, 3, 0, 3, 2})},
		{'-', new LetterData(new float[]{0.8f, 1.0f, 0.2f, 1.0f}, new int[]{0, 1})},
		{'.', new LetterData(new float[]{0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.5f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0})},
		{'/', new LetterData(new float[]{0.9f, 1.9f, 0.1f, 0.1f}, new int[]{0, 1})},
		{'0', new LetterData(new float[]{0.1f, 1.9f, 0.9f, 1.9f, 0.1f, 0.1f, 0.9f, 0.1f, 0.1f, 0.1f, 0.9f, 1.9f}, new int[]{0, 1, 2, 0, 1, 3, 3, 2, 4, 5})},
		{'1', new LetterData(new float[]{0.9f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1})},
		{'2', new LetterData(new float[]{0.1f, 1.9f, 0.9f, 1.9f, 0.1f, 0.1f, 0.9f, 0.1f, 0.9f, 1.0f, 0.1f, 1.0f}, new int[]{0, 1, 2, 3, 1, 4, 4, 5, 2, 5})},
		{'3', new LetterData(new float[]{0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f, 0.1f, 0.1f, 0.1f, 1.0f, 0.9f, 1.0f}, new int[]{0, 1, 1, 2, 2, 3, 4, 5})},
		{'4', new LetterData(new float[]{0.1f, 1.9f, 0.1f, 1.0f, 0.9f, 1.9f, 0.9f, 0.1f, 0.9f, 1.0f}, new int[]{0, 1, 2, 3, 1, 4})},
		{'5', new LetterData(new float[]{0.1f, 1.0f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 1.0f, 0.9f, 0.1f, 0.1f, 0.1f}, new int[]{0, 1, 1, 2, 3, 0, 3, 4, 4, 5})},
		{'6', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 1.0f, 0.9f, 0.1f, 0.1f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 5, 3})},
		{'7', new LetterData(new float[]{0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1, 1, 2})},
		{'8', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f, 0.1f, 1.0f, 0.9f, 1.0f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5})},
		{'9', new LetterData(new float[]{0.1f, 1.0f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f, 0.9f, 1.0f, 0.1f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3, 4, 0, 3, 5})},
		{':', new LetterData(new float[]{0.4f, 1.4f, 0.5f, 1.5f, 0.6f, 1.4f, 0.5f, 1.3f, 0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.5f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4})},
		{';', new LetterData(new float[]{0.4f, 1.4f, 0.5f, 1.5f, 0.6f, 1.4f, 0.5f, 1.3f, 0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.4f, 0.0f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4})},
		{'<', new LetterData(new float[]{0.8f, 0.7f, 0.2f, 1.0f, 0.8f, 1.3f}, new int[]{0, 1, 1, 2})},
		{'=', new LetterData(new float[]{0.2f, 1.2f, 0.8f, 1.2f, 0.2f, 0.8f, 0.8f, 0.8f}, new int[]{0, 1, 2, 3})},
		{'>', new LetterData(new float[]{0.2f, 1.3f, 0.8f, 1.0f, 0.2f, 0.7f}, new int[]{0, 1, 1, 2})},
		{'?', new LetterData(new float[]{0.4f, 0.2f, 0.5f, 0.3f, 0.6f, 0.2f, 0.5f, 0.1f, 0.5f, 0.5f, 0.5f, 0.8f, 0.9f, 1.2f, 0.9f, 1.7f, 0.7f, 1.9f, 0.3f, 1.9f, 0.1f, 1.7f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10})},
		{'@', new LetterData(new float[]{0.1f, 1.7f, 0.3f, 1.9f, 0.7f, 1.9f, 0.1f, 0.3f, 0.9f, 1.7f, 0.3f, 0.1f, 0.7f, 0.1f, 0.9f, 0.3f, 0.9f, 1.0f, 0.7f, 0.8f, 0.5f, 0.8f, 0.3f, 1.0f, 0.3f, 1.2f, 0.5f, 1.4f}, new int[]{0, 1, 1, 2, 3, 0, 2, 4, 5, 3, 6, 5, 7, 6, 4, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13})},
		{'A', new LetterData(new float[]{0.1f, 0.1f, 0.5f, 1.9f, 0.9f, 0.1f, 0.3f, 1.0f, 0.7f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4})},
		{'B', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.7f, 1.9f, 0.9f, 0.3f, 0.7f, 0.1f, 0.9f, 0.8f, 0.7f, 1.0f, 0.1f, 1.0f, 0.9f, 1.7f, 0.9f, 1.2f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 5, 3, 6, 5, 7, 6, 2, 8, 8, 9, 6, 9})},
		{'C', new LetterData(new float[]{0.1f, 0.3f, 0.1f, 1.7f, 0.3f, 1.9f, 0.7f, 0.1f, 0.3f, 0.1f, 0.9f, 0.3f, 0.7f, 1.9f, 0.9f, 1.7f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 5, 3, 2, 6, 6, 7})},
		{'D', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.9f, 0.3f, 0.7f, 0.1f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 4, 5, 0, 5})},
		{'E', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.6f, 1.0f, 0.1f, 1.0f, 0.9f, 0.1f}, new int[]{0, 1, 1, 2, 3, 4, 5, 0})},
		{'F', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.6f, 1.0f, 0.1f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4})},
		{'G', new LetterData(new float[]{0.1f, 0.3f, 0.1f, 1.7f, 0.3f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.3f, 0.1f, 0.7f, 0.1f, 0.9f, 0.3f, 0.9f, 0.6f, 0.7f, 0.6f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 5, 0, 6, 5, 7, 6, 8, 7, 9, 8})},
		{'H', new LetterData(new float[]{0.1f, 1.0f, 0.9f, 1.0f, 0.9f, 1.9f, 0.9f, 0.1f, 0.1f, 1.9f, 0.1f, 0.1f}, new int[]{0, 1, 2, 3, 4, 5})},
		{'I', new LetterData(new float[]{0.3f, 0.1f, 0.7f, 0.1f, 0.5f, 1.9f, 0.5f, 0.1f, 0.3f, 1.9f, 0.7f, 1.9f}, new int[]{0, 1, 2, 3, 4, 5})},
		{'J', new LetterData(new float[]{0.5f, 0.1f, 0.3f, 0.3f, 0.3f, 1.9f, 0.7f, 1.9f, 0.7f, 1.9f, 0.7f, 0.3f}, new int[]{0, 1, 2, 3, 4, 5, 5, 0})},
		{'K', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.1f, 1.0f, 0.9f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1, 2, 3, 4, 2})},
		{'L', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1, 2, 0})},
		{'M', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.5f, 1.0f, 0.9f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3, 3, 4})},
		{'N', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 0.1f, 0.9f, 1.9f}, new int[]{0, 1, 2, 3, 1, 2})},
		{'O', new LetterData(new float[]{0.1f, 0.3f, 0.1f, 1.7f, 0.7f, 1.9f, 0.9f, 1.7f, 0.3f, 1.9f, 0.9f, 0.3f, 0.7f, 0.1f, 0.3f, 0.1f}, new int[]{0, 1, 2, 3, 4, 2, 1, 4, 3, 5, 5, 6, 7, 0, 6, 7})},
		{'P', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.9f, 1.2f, 0.7f, 1.0f, 0.1f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 4, 5, 5, 6})},
		{'Q', new LetterData(new float[]{0.1f, 0.3f, 0.1f, 1.7f, 0.3f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.9f, 0.4f, 0.3f, 0.1f, 0.6f, 0.1f, 0.6f, 0.4f, 0.9f, 0.1f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 4, 5, 6, 0, 7, 6, 5, 7, 8, 9})},
		{'R', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.9f, 1.2f, 0.7f, 1.0f, 0.9f, 0.1f, 0.5f, 1.0f, 0.1f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 4, 5, 6, 7, 5, 8})},
		{'S', new LetterData(new float[]{0.1f, 1.2f, 0.1f, 1.7f, 0.3f, 1.9f, 0.7f, 1.9f, 0.9f, 1.7f, 0.3f, 1.0f, 0.7f, 1.0f, 0.9f, 0.8f, 0.9f, 0.3f, 0.7f, 0.1f, 0.3f, 0.1f, 0.1f, 0.3f}, new int[]{0, 1, 1, 2, 3, 4, 2, 3, 5, 0, 6, 5, 7, 6, 8, 7, 9, 8, 10, 9, 11, 10})},
		{'T', new LetterData(new float[]{0.1f, 1.9f, 0.9f, 1.9f, 0.5f, 1.9f, 0.5f, 0.1f}, new int[]{0, 1, 2, 3})},
		{'U', new LetterData(new float[]{0.1f, 0.3f, 0.1f, 1.9f, 0.9f, 0.3f, 0.7f, 0.1f, 0.3f, 0.1f, 0.9f, 1.9f}, new int[]{0, 1, 2, 3, 3, 4, 4, 0, 5, 2})},
		{'V', new LetterData(new float[]{0.5f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f}, new int[]{0, 1, 2, 0})},
		{'W', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f, 0.5f, 0.6f}, new int[]{0, 1, 2, 3, 3, 4, 4, 0})},
		{'X', new LetterData(new float[]{0.9f, 0.1f, 0.1f, 1.9f, 0.1f, 0.1f, 0.9f, 1.9f}, new int[]{0, 1, 2, 3})},
		{'Y', new LetterData(new float[]{0.5f, 1.0f, 0.1f, 1.9f, 0.5f, 0.1f, 0.9f, 1.9f}, new int[]{0, 1, 0, 2, 3, 0})},
		{'Z', new LetterData(new float[]{0.9f, 1.9f, 0.1f, 1.9f, 0.9f, 0.1f, 0.1f, 0.1f}, new int[]{0, 1, 2, 3, 3, 0})},
		{'[', new LetterData(new float[]{0.4f, 0.1f, 0.4f, 1.9f, 0.6f, 1.9f, 0.6f, 0.1f}, new int[]{0, 1, 1, 2, 3, 0})},
		{'\\', new LetterData(new float[]{0.9f, 0.1f, 0.1f, 1.9f}, new int[]{0, 1})},
		{'\x007f', new LetterData(new float[]{0.1f, 0.1f, 0.1f, 1.9f, 0.9f, 1.9f, 0.9f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 1, 3, 2, 0})},
		{']', new LetterData(new float[]{0.4f, 1.9f, 0.6f, 1.9f, 0.6f, 0.1f, 0.4f, 0.1f}, new int[]{0, 1, 1, 2, 2, 3})},
		{'^', new LetterData(new float[]{0.3f, 1.7f, 0.5f, 1.9f, 0.7f, 1.7f}, new int[]{0, 1, 1, 2})},
		{'_', new LetterData(new float[]{0.9f, 0.1f, 0.1f, 0.1f}, new int[]{0, 1})},
		{'`', new LetterData(new float[]{0.6f, 1.5f, 0.4f, 1.7f, 0.5f, 1.8f, 0.6f, 1.7f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0})},
		{'a', new LetterData(new float[]{0.8f, 0.3f, 0.6f, 0.1f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 1.0f, 0.8f, 0.8f, 0.9f, 0.1f, 0.4f, 0.1f, 0.2f, 0.3f, 0.2f, 0.5f, 0.4f, 0.7f, 0.6f, 0.7f, 0.8f, 0.5f}, new int[]{0, 1, 2, 3, 4, 5, 3, 4, 5, 0, 0, 6, 1, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12})},
		{'b', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.4f, 0.8f, 0.3f, 0.8f, 0.7f, 0.6f, 0.1f, 0.4f, 0.1f, 0.6f, 0.9f, 0.4f, 0.9f, 0.2f, 0.7f, 0.2f, 0.3f}, new int[]{0, 1, 2, 3, 4, 2, 5, 4, 3, 6, 6, 7, 7, 8, 9, 5})},
		{'c', new LetterData(new float[]{0.2f, 0.3f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 1.0f, 0.4f, 0.1f, 0.8f, 0.8f, 0.6f, 0.1f, 0.8f, 0.3f}, new int[]{0, 1, 2, 3, 1, 2, 4, 0, 3, 5, 6, 4, 7, 6})},
		{'d', new LetterData(new float[]{0.2f, 0.3f, 0.2f, 0.7f, 0.4f, 0.9f, 0.6f, 0.1f, 0.4f, 0.1f, 0.6f, 0.9f, 0.8f, 0.7f, 0.8f, 0.3f, 0.8f, 0.1f, 0.8f, 1.4f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 2, 5, 5, 6, 7, 3, 8, 9})},
		{'e', new LetterData(new float[]{0.4f, 0.1f, 0.2f, 0.3f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 1.0f, 0.6f, 0.1f, 0.8f, 0.3f, 0.8f, 0.8f, 0.8f, 0.6f, 0.2f, 0.6f}, new int[]{0, 1, 2, 3, 3, 4, 1, 2, 5, 0, 6, 5, 4, 7, 7, 8, 8, 9})},
		{'f', new LetterData(new float[]{0.9f, 1.2f, 0.7f, 1.4f, 0.3f, 1.0f, 0.7f, 1.0f, 0.5f, 1.2f, 0.5f, 0.1f}, new int[]{0, 1, 2, 3, 4, 5, 1, 4})},
		{'g', new LetterData(new float[]{0.4f, -0.1f, 0.2f, 0.1f, 0.6f, -0.1f, 0.8f, 1.0f, 0.8f, 0.1f, 0.6f, 1.0f, 0.8f, 0.8f, 0.4f, 1.0f, 0.2f, 0.8f, 0.2f, 0.4f, 0.4f, 0.2f, 0.6f, 0.2f, 0.8f, 0.4f}, new int[]{0, 1, 2, 0, 3, 4, 4, 2, 5, 6, 7, 5, 8, 7, 9, 8, 10, 9, 11, 10, 12, 11})},
		{'h', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.4f, 0.6f, 1.0f, 0.8f, 0.8f, 0.4f, 1.0f, 0.2f, 0.8f, 0.8f, 0.1f}, new int[]{0, 1, 2, 3, 4, 2, 5, 4, 3, 6})},
		{'i', new LetterData(new float[]{0.5f, 0.1f, 0.5f, 1.0f, 0.5f, 1.3f, 0.6f, 1.2f, 0.4f, 1.2f, 0.5f, 1.1f}, new int[]{0, 1, 2, 3, 4, 2, 5, 4, 3, 5})},
		{'j', new LetterData(new float[]{0.6f, 1.2f, 0.7f, 1.3f, 0.8f, 1.2f, 0.7f, 1.1f, 0.7f, 1.0f, 0.7f, 0.3f, 0.5f, 0.1f, 0.3f, 0.3f}, new int[]{0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7})},
		{'k', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.4f, 0.2f, 0.6f, 0.8f, 0.1f, 0.8f, 1.0f}, new int[]{0, 1, 2, 3, 4, 2})},
		{'l', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.4f}, new int[]{0, 1})},
		{'m', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.0f, 0.8f, 0.8f, 0.6f, 1.0f, 0.8f, 0.1f, 0.5f, 0.9f, 0.5f, 0.1f, 0.4f, 1.0f, 0.2f, 0.8f}, new int[]{0, 1, 2, 3, 4, 2, 5, 6, 7, 8, 5, 3, 7, 5})},
		{'n', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.0f, 0.6f, 1.0f, 0.4f, 1.0f, 0.8f, 0.8f, 0.8f, 0.1f, 0.2f, 0.8f}, new int[]{0, 1, 2, 3, 4, 2, 5, 4, 3, 6})},
		{'o', new LetterData(new float[]{0.2f, 0.3f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 0.1f, 0.4f, 0.1f, 0.8f, 0.3f, 0.8f, 0.8f, 0.6f, 1.0f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 5, 3, 6, 5, 7, 6, 2, 7})},
		{'p', new LetterData(new float[]{0.2f, 1.0f, 0.2f, 0.1f, 0.4f, 1.0f, 0.6f, 1.0f, 0.2f, 0.8f, 0.4f, 0.2f, 0.2f, 0.4f, 0.8f, 0.8f, 0.8f, 0.4f, 0.6f, 0.2f}, new int[]{0, 1, 2, 3, 4, 2, 5, 6, 3, 7, 7, 8, 8, 9, 5, 9})},
		{'q', new LetterData(new float[]{0.2f, 0.3f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 0.1f, 0.4f, 0.1f, 0.8f, 0.3f, 0.6f, 1.0f, 0.8f, 0.8f, 0.8f, 1.0f, 0.8f, -0.1f}, new int[]{0, 1, 1, 2, 3, 4, 4, 0, 5, 3, 2, 6, 6, 7, 8, 9})},
		{'r', new LetterData(new float[]{0.2f, 0.1f, 0.2f, 1.0f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 1.0f, 0.8f, 0.8f}, new int[]{0, 1, 2, 3, 4, 5, 3, 4})},
		{'s', new LetterData(new float[]{0.2f, 0.7f, 0.2f, 0.8f, 0.4f, 1.0f, 0.6f, 1.0f, 0.3f, 0.6f, 0.7f, 0.6f, 0.8f, 0.8f, 0.8f, 0.5f, 0.8f, 0.3f, 0.6f, 0.1f, 0.4f, 0.1f, 0.2f, 0.3f}, new int[]{0, 1, 2, 3, 1, 2, 4, 5, 3, 6, 0, 4, 5, 7, 7, 8, 8, 9, 9, 10, 10, 11})},
		{'t', new LetterData(new float[]{0.5f, 0.3f, 0.5f, 1.4f, 0.3f, 1.0f, 0.7f, 1.0f, 0.9f, 0.3f, 0.7f, 0.1f}, new int[]{0, 1, 2, 3, 4, 5, 5, 0})},
		{'u', new LetterData(new float[]{0.2f, 0.3f, 0.2f, 1.0f, 0.8f, 0.3f, 0.6f, 0.1f, 0.4f, 0.1f, 0.8f, 1.0f, 0.8f, 0.1f}, new int[]{0, 1, 2, 3, 3, 4, 4, 0, 5, 6})},
		{'v', new LetterData(new float[]{0.5f, 0.1f, 0.2f, 1.0f, 0.8f, 1.0f}, new int[]{0, 1, 2, 0})},
		{'w', new LetterData(new float[]{0.3f, 0.1f, 0.2f, 1.0f, 0.8f, 1.0f, 0.7f, 0.1f, 0.5f, 0.4f}, new int[]{0, 1, 2, 3, 3, 4, 4, 0})},
		{'x', new LetterData(new float[]{0.8f, 0.1f, 0.2f, 1.0f, 0.8f, 1.0f, 0.2f, 0.1f}, new int[]{0, 1, 2, 3})},
		{'y', new LetterData(new float[]{0.5f, 0.6f, 0.5f, 0.1f, 0.2f, 1.0f, 0.8f, 1.0f}, new int[]{0, 1, 0, 2, 3, 0})},
		{'z', new LetterData(new float[]{0.8f, 1.0f, 0.2f, 1.0f, 0.8f, 0.1f, 0.2f, 0.1f}, new int[]{0, 1, 2, 3, 3, 0})},
		{'{', new LetterData(new float[]{0.4f, 1.7f, 0.6f, 1.9f, 0.4f, 0.9f, 0.3f, 1.0f, 0.4f, 1.1f, 0.4f, 0.3f, 0.6f, 0.1f}, new int[]{0, 1, 2, 3, 3, 4, 4, 0, 5, 2, 6, 5})},
		{'|', new LetterData(new float[]{0.5f, 0.1f, 0.5f, 1.9f}, new int[]{0, 1})},
		{'}', new LetterData(new float[]{0.7f, 1.0f, 0.6f, 0.9f, 0.6f, 1.1f, 0.4f, 1.9f, 0.6f, 1.7f, 0.6f, 0.3f, 0.4f, 0.1f}, new int[]{0, 1, 2, 0, 3, 4, 4, 2, 1, 5, 5, 6})},
		{'~', new LetterData(new float[]{0.4f, 1.7f, 0.3f, 1.6f, 0.7f, 1.6f, 0.6f, 1.5f}, new int[]{0, 1, 2, 3, 3, 0})},
	};
	static readonly Vector2 charSize = new Vector2(1.0f, 2.0f);
	static readonly char defaultChar = '\x007f';

	//static Dictionary<char, LetterData> letters = null;

	static int countLines(string text){
		if (string.IsNullOrEmpty(text))
			return 0;

		int result = 1; //I should probably just linq the heck out of it...
		for(int i = 0; i < text.Length; i++)
			if (text[i] == '\n')
				result++;

		return result;
	}

	static int getMaxLineLength(string text){
		if (string.IsNullOrEmpty(text))
			return 0;

		int result = 0;
		int curValue = 0;
		for(int i = 0; i < text.Length; i++){
			if (text[i] != '\n'){
				curValue++;	
			}
			else{
				result = Mathf.Max(curValue, result);
				curValue = 0;
			}			
		}
		result = Mathf.Max(curValue, result);
		return result;
	}

	static int getCurLineLength(string text, int startPos){
		if (string.IsNullOrEmpty(text))
			return 0;

		var curLength = 0;
		for(int i = startPos; i < text.Length; i++){
			if (text[i] == '\n')
				return curLength;
			curLength++;
		}
		return curLength;
	}

	static float getAnchorXPivot(TextAnchor anchor){
		if ((anchor == TextAnchor.LowerCenter) || (anchor == TextAnchor.MiddleCenter) || (anchor == TextAnchor.UpperCenter))
			return 0.5f;
		if ((anchor == TextAnchor.LowerRight) || (anchor == TextAnchor.MiddleRight) || (anchor == TextAnchor.UpperRight))
			return 1.0f;
		return 0.0f;
	}

	public static void processTextLine(string text, LineDelegate2D callback){
		if (string.IsNullOrEmpty(text))
			return;
		if (callback == null)
			throw new System.ArgumentNullException();

		var charOffset = Vector2.zero;
		for(int i = 0; i < text.Length; i++){
			char c = text[i];
			processLetter(c, 
				(a, b) => 
					callback(a + charOffset, b + charOffset));
			charOffset.x += charSize.x;
		}
	}

	public static void processText(string text, TextAnchor anchor, TextAnchor alignment, LineDelegate2D callback){
		if (string.IsNullOrEmpty(text))
			return;
		if (callback == null)
			throw new System.ArgumentNullException();
		Vector2 textOffset = new Vector2(0.0f, 0.0f);
		int numLines = countLines(text);
		int maxLineLength = getMaxLineLength(text);

		float maxLineWidth = charSize.x * (float)maxLineLength;
		if ((anchor == TextAnchor.UpperCenter) || (anchor == TextAnchor.UpperLeft) || (anchor == TextAnchor.UpperRight)){
			textOffset.y -= charSize.y;			
		}
		else if ((anchor == TextAnchor.MiddleCenter) || (anchor == TextAnchor.MiddleLeft) || (anchor == TextAnchor.MiddleRight)){
			textOffset.y = (float)numLines * charSize.y * 0.5f - charSize.y;
		}
		else{
			textOffset.y = (float)numLines * charSize.y;
		}

		int charOffset = 0;

		float lineAnchorOffsetMultiplier =getAnchorXPivot(anchor);
		float lineAlignmentOffsetMultiplier = getAnchorXPivot(alignment);

		var lineOffset = textOffset;
		do{
			var curLineLength = getCurLineLength(text, charOffset);
			float lineWidth = charSize.x * (float)curLineLength;

			lineOffset.x = textOffset.x - maxLineWidth * lineAnchorOffsetMultiplier 
				+ (maxLineWidth - lineWidth) * lineAlignmentOffsetMultiplier;
			for(int i = 0; i < curLineLength; i++){
				char c = text[i + charOffset];
				processLetter(c, 
					(p1, p2) =>
						callback(p1 + lineOffset, p2 + lineOffset)
				);
				lineOffset.x += charSize.x;
			}

			lineOffset.y -= charSize.y;
			charOffset += curLineLength + 1;
		}while(charOffset < text.Length);
	}

	public static bool processLetter(char letter, LineDelegate2D callback){
		if (callback == null)
			throw new System.ArgumentNullException();

		if (!letters.ContainsKey(letter)){
			if (letter != defaultChar)
				processLetter(defaultChar, callback);
			return false;
		}

		var curLetterData = letters[letter];

		for (int i = 0; i < curLetterData.ints.Length; i+= 2){
			int idxa = curLetterData.ints[i];
			int idxb = curLetterData.ints[i+1];

			callback(curLetterData.getVertex(idxa), curLetterData.getVertex(idxb));
		}

		return true;
	}
}

}
