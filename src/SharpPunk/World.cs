using System.Linq;
using System.Collections.Generic;

namespace SharpPunk
{
	public class World : Tweener
	{
		public World()
		{
			m_entities = new List<Entity>();
		}

		public void Update()
		{
			foreach (Entity e in m_entities)
				e.Update();
		}

		public void Render()
		{
			// TODO: Support layers, render ordering
			foreach (Entity e in Enumerable.Reverse(m_entities))
				e.Render();
		}

		public Entity Add(Entity e)
		{
			if (e.World != null)
				return e;

			// TODO: Queue for addition on the next frame
			m_entities.Add(e);
			e.World = this;

			return e;
		}

		public void AddRange(IEnumerable<Entity> entities)
		{
			foreach (Entity e in entities)
				Add(e);
		}

		public void Add(params Entity[] entities)
		{
			AddRange(entities);
		}

		public Entity Remove(Entity e)
		{
			if (e.World != this)
				return e;

			// TODO: Queue for removal on the next frame
			m_entities.Remove(e);
			e.World = null;
			
			return e;
		}

		public void RemoveRange(IEnumerable<Entity> entities)
		{
			foreach (Entity e in entities)
				Remove(e);
		}

		public void Remove(params Entity[] entities)
		{
			RemoveRange(entities);
		}

		public void RemoveAll()
		{
			RemoveRange(m_entities);
		}

		protected void OnBegin()
		{
		}

		protected void OnEnd()
		{
		}

		readonly List<Entity> m_entities;
	}
}