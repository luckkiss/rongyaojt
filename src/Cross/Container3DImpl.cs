using System;
using System.Collections.Generic;

namespace Cross
{
	public class Container3DImpl : GraphObject3DImpl, IContainer3D, IGraphObject3D, IGraphObject
	{
		protected List<GraphObject3DImpl> m_children = new List<GraphObject3DImpl>();

		public int numChildren
		{
			get
			{
				return this.m_children.Count;
			}
		}

		public Container3DImpl()
		{
			this.m_u3dObj.name = "Container3D";
		}

		public override void dispose()
		{
			bool flag = this.m_children != null;
			if (flag)
			{
				for (int i = 0; i < this.m_children.Count; i++)
				{
					this.m_children[i].dispose();
				}
				this.m_children = null;
				base.dispose();
			}
			base.dispose();
		}

		public void addChild(IGraphObject3D child)
		{
			bool flag = child == null;
			if (!flag)
			{
				GraphObject3DImpl graphObject3DImpl = child as GraphObject3DImpl;
				bool flag2 = this.m_children.Contains(graphObject3DImpl);
				if (!flag2)
				{
					this.m_children.Add(graphObject3DImpl);
					bool flag3 = graphObject3DImpl.parent != null;
					if (flag3)
					{
						graphObject3DImpl.parent.removeChild(child);
					}
					graphObject3DImpl.parent = this;
				}
			}
		}

		public void addChildAt(IGraphObject3D child, int idx)
		{
			bool flag = child == null;
			if (!flag)
			{
				GraphObject3DImpl graphObject3DImpl = child as GraphObject3DImpl;
				bool flag2 = graphObject3DImpl.parent != null;
				if (flag2)
				{
					graphObject3DImpl.parent.removeChild(child);
				}
				bool flag3 = idx >= this.m_children.Count;
				if (flag3)
				{
					this.m_children.Add(graphObject3DImpl);
				}
				else
				{
					this.m_children.Insert(idx, graphObject3DImpl);
				}
				graphObject3DImpl.parent = this;
			}
		}

		public void removeChild(IGraphObject3D child)
		{
			bool flag = child == null;
			if (!flag)
			{
				GraphObject3DImpl graphObject3DImpl = child as GraphObject3DImpl;
				bool flag2 = !this.m_children.Contains(graphObject3DImpl);
				if (!flag2)
				{
					this.m_children.Remove(graphObject3DImpl);
					graphObject3DImpl.parent = null;
				}
			}
		}

		public void removeChildAt(int idx)
		{
			bool flag = idx < 0 || idx >= this.m_children.Count;
			if (!flag)
			{
				GraphObject3DImpl graphObject3DImpl = this.m_children[idx];
				this.m_children.RemoveAt(idx);
				bool flag2 = graphObject3DImpl != null;
				if (flag2)
				{
					graphObject3DImpl.parent = null;
				}
			}
		}

		public IGraphObject3D getChildAt(int idx)
		{
			bool flag = idx < 0 || idx >= this.m_children.Count;
			IGraphObject3D result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_children[idx];
			}
			return result;
		}

		public int indexOf(IGraphObject3D child)
		{
			bool flag = child == null;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				GraphObject3DImpl item = child as GraphObject3DImpl;
				result = this.m_children.IndexOf(item);
			}
			return result;
		}

		public override void _updateRealVisible()
		{
			base._updateRealVisible();
			foreach (GraphObject3DImpl current in this.m_children)
			{
				current._updateRealVisible();
			}
		}

		public bool contains(IGraphObject3D obj)
		{
			bool flag = this.m_children.Contains(obj as GraphObject3DImpl);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < this.m_children.Count; i++)
				{
					GraphObject3DImpl graphObject3DImpl = this.m_children[i];
					bool flag2 = graphObject3DImpl != null && graphObject3DImpl is Container3DImpl;
					if (flag2)
					{
						bool flag3 = (graphObject3DImpl as Container3DImpl).contains(obj);
						if (flag3)
						{
							result = true;
							return result;
						}
					}
				}
				result = false;
			}
			return result;
		}
	}
}
