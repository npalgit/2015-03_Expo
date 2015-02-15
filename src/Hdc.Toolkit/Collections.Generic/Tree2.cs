namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    public class Tree<TTree> : ITree<TTree> where TTree : class , ITree<TTree>
    {
        private IList<TTree> _childNodes = new List<TTree>();

        private TTree _parentNode;

        protected Tree(params TTree[] children) : this(children as IEnumerable<TTree>)
        {
        }

        protected Tree(IEnumerable<TTree> children)
        {
            var childNodes = new List<TTree>();

            _childNodes = childNodes;

            children.ArgumentIsNotNull("children");
            foreach (var child in children)
            {
                Add(child);
            }
        }

        public List<TTree> ConcreteChildren
        {
            get { return _childNodes as List<TTree>; }
        }


        public IList<TTree> ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }


        public TTree ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }


        public int Degree
        {
            get { return _childNodes.Count; }
        }


        public int Depth
        {
            get { return GetLevel(); }
        }


        public bool IsLeafNode
        {
            get { return _childNodes.Count == 0; }
        }


        public bool IsRootNode
        {
            get { return _parentNode == null; }
        }


        public bool IsReadOnly
        {
            get { return _childNodes.IsReadOnly; }
        }


        public int Count
        {
            get { return _childNodes.Count; }
        }


        public int Index
        {
            get
            {
                if (_parentNode == null)
                    return 0;

                return _parentNode.ChildNodes.IndexOf(this as TTree);
            }
            set { throw new NotImplementedException(); }
        }


        public TTree this[int index]
        {
            get { return _childNodes[index]; }
            set { _childNodes[index] = value; }
        }


        public void Add(TTree child)
        {
            _childNodes.Add(child);
            child.ParentNode = this as TTree;
        }

        public void CopyTo(TTree[] array, int arrayIndex)
        {
            _childNodes.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(TTree child)
        {
            var isRemoved = _childNodes.Remove(child);
            if (isRemoved)
                child.ParentNode = null;

            return isRemoved;
        }


        public int IndexOf(TTree item)
        {
            return _childNodes.IndexOf(item);
        }

        public void Insert(int index, TTree item)
        {
            _childNodes.Insert(index, item);
            item.ParentNode = this as TTree;

            var lastItems = _childNodes.Select(node => _childNodes.IndexOf(node) > index).ToList();
        }

        public void RemoveAt(int index)
        {
            Remove(_childNodes[index]);
        }

        public void Clear()
        {
            var children = _childNodes.ToList<TTree>();
            foreach (var child in children)
            {
                Remove(child);
            }
        }

        public bool Contains(TTree item)
        {
            return _childNodes.Contains(item);
        }


        public IList<TTree> GetAncestors()
        {
            var path = new List<TTree>();

            for (var node = ParentNode; node != null; node = node.ParentNode)
            {
                path.Add(node);
            }

            path.Reverse();

            return path;
        }

        public IList<TTree> GetAncestorsIncludeSelf()
        {
            var ancestors = GetAncestors();
            ancestors.Add(this as TTree);

            return ancestors;
        }

        public TTree GetAncestor(int upLevel)
        {
            if (upLevel < 0)
                return null;
            var ans = GetAncestorsIncludeSelf().Reverse().ToList<TTree>();
            return upLevel < ans.Count ? ans[upLevel] : null;
        }

        public IEnumerable<TTree> GetDescendants()
        {
            var stack = new Stack<TTree>();

            stack.Push(this as TTree);

            while (stack.Count > 0)
            {
                var tree = stack.Pop();

                if (tree != null)
                {
                    if (!Equals(tree, this))
                        yield return tree;

                    for (var i = 0; i < tree.ChildNodes.Count; i++)
                    {
                        stack.Push(tree.ChildNodes[i]);
                    }
                }
            }
        }


        public IEnumerable<TTree> GetDescendantsIncludeSelf()
        {
            var descendants = GetDescendants().ToList<TTree>();
            descendants.Insert(0, this as TTree);
            return descendants;
        }

        public int GetLevel()
        {
            return GetAncestors().Count;
        }


        public TTree FindNode(Predicate<TTree> condition)
        {
            foreach (var childNode in _childNodes)
            {
                if (condition(childNode))
                    return childNode;
            }

            return null;
        }
    }
}