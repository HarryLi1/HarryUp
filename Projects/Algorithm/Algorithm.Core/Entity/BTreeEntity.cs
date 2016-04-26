using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core.Entity
{
    public class BTreeEntity
    {
        public int Value;
        public BTreeEntity Left;
        public BTreeEntity Right;

        public override string ToString()
        {
            if (this == null) return "#";
            return string.Format("{0}{1}{2}", 
                Value, 
                Left == null ? "#" : Left.ToString(), 
                Right == null ? "#" : Right.ToString()
                );
        }
    }

    public class TagBTreeEntity
    {
        public BTreeEntity Tree;
        public bool IsRightProcessed;
    }
}
