﻿using System;
using UnityEngine;

namespace TechnomediaLabs
{
	public struct IntersectPoints
	{
		public Vector3 first;
		public Vector3 second;

		public IntersectPoints(Vector3 first, Vector3 second)
		{
			this.first = first;
			this.second = second;
		}
	}
}