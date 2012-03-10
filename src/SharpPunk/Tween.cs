using System;

namespace SharpPunk
{
	public class Tween
	{
		public Tweener Parent { get; set; }

		public bool Active
		{
			get { return m_active; }
			set { m_active = value; }
		}

		public bool Finished
		{
			get { return m_finished; }
		}

		public float Scale
		{
			get { return m_timeScale; }
		}

		public float Percent
		{
			get { return m_currentTime / m_duration; }
			set { m_currentTime = m_duration * value; }
		}

		public Tween(float duration, TweenType type, Action onComplete, Func<float, float> onEase)
		{
			m_timeScale = 0;
			m_duration = duration;
			m_type = type;
			m_onComplete = onComplete;
			m_onEase = onEase;
		}

		internal void Update()
		{
			// TODO: Get actual time elapsed, fixed or not
			m_currentTime += 1;
			m_timeScale = m_currentTime / m_duration;

			if (m_onEase != null && m_timeScale > 0 && m_timeScale < 1)
				m_timeScale = m_onEase(m_timeScale);
			
			if (m_currentTime >= m_duration)
			{
				m_timeScale = 1;
				m_finished = true;
			}
		}

		internal void Finish()
		{
			switch(m_type)
			{
			case TweenType.Persist:
				m_currentTime = m_duration;
				m_active = false;
				break;
			case TweenType.Looping:
				m_currentTime %= m_duration;
				m_timeScale = m_currentTime / m_duration;
				if (m_onEase != null && m_timeScale > 0 && m_timeScale < 1)
					m_timeScale = m_onEase(m_timeScale);
				Start();
				break;
			case TweenType.OneShot:
				m_currentTime = m_duration;
				m_active = false;
				Parent.RemoveTween(this);
				break;
			}

			m_finished = false;
			if (m_onComplete != null)
				m_onComplete();
		}

		public void Start()
		{
			m_currentTime = 0;
			if (Math.Abs(m_duration - 0) < 0.0001f)
			{
				m_active = false;
				return;
			}

			m_active = true;
		}

		readonly TweenType m_type;
		readonly Action m_onComplete;
		readonly Func<float, float> m_onEase;
		readonly float m_duration;

		bool m_active;
		bool m_finished;
		float m_currentTime;
		float m_timeScale;
	}
}