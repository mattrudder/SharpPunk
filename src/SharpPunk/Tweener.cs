using System;
using System.Collections.Generic;

namespace SharpPunk
{
	public abstract class Tweener
	{
		public void AddTween(Tween t)
		{
			AddTween(t, false);
		}

		public void AddTween(Tween t, bool start)
		{
			if (t.Parent != null)
				throw new ArgumentException("Cannot add a Tween object more than once.");

			t.Parent = this;
			m_tweens.Insert(0, t);

			if (start)
				t.Start();
		}

		public void RemoveTween(Tween t)
		{
			int index = m_tweens.IndexOf(t);
			if (index == -1)
				throw new ArgumentException("Core object does not contain Tween.");

			t.Active = false;
			m_tweens.RemoveAt(index);
		}

		public void ClearTweens()
		{
			foreach (Tween t in m_tweens)
				RemoveTween(t);
		}

		public void UpdateTweens()
		{
			foreach (Tween t in m_tweens)
			{
				if (t.Active)
				{
					t.Update();
					if (t.Finished)
						t.Finish();
				}
			}
		}

		protected Tweener()
		{
			m_tweens = new List<Tween>();
		}

		readonly List<Tween> m_tweens;
	}
}