using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeVisitor {

    public class AbstractVisitor {
        public virtual void VisitChild1(Child1 node){}
        public virtual void VisitChild2(Child2 node){}

        public void VisitChildren(CompositeAbstractNode node) {

        }
    }


    public class GraphVizPrinterVisitor : AbstractVisitor {
        public override void VisitChild1(Child1 node) {
            base.VisitChild1(node);
        }

        public override void VisitChild2(Child2 node) {
            base.VisitChild2(node);
        }
    }






    public abstract class CompositeAbstractNode {
        public enum NodeTypes{ T_TYPE1, T_TYPE2}
        private CompositeAbstractNode m_parent;
        private List<CompositeAbstractNode> m_children;
        private NodeTypes m_nodeType;
        private int m_serial;
        private static int m_serialCounter = 0;
        // ST PRINTER
        private string m_graphVizName;

        public CompositeAbstractNode(NodeTypes nodeType) {
            m_nodeType = nodeType;
            m_serial = m_serialCounter++;
            m_children = new List<CompositeAbstractNode>();
        }

        public CompositeAbstractNode GetParent() {
            return m_parent;
        }

        public void AddChild(CompositeAbstractNode child) {
            m_children.Add(child);
            child.m_parent = this;
        }

        public CompositeAbstractNode GetChild(int index) {
            return m_children[index];
        }

        public virtual void GraphVizPrinter() {
            foreach (CompositeAbstractNode node in m_children) {
                node.GraphVizPrinter();
            }
        }

        public abstract void Accept(AbstractVisitor visitor);
    }

    public class Child1 : CompositeAbstractNode {
        public Child1( ) : base(NodeTypes.T_TYPE1) {
        }

        public override void GraphVizPrinter() {
            base.GraphVizPrinter();
        }

        public override void Accept(AbstractVisitor visitor) {
            visitor.VisitChild1(this);
        }
    }

    public class Child2 : CompositeAbstractNode {
        public Child2( ) : base(NodeTypes.T_TYPE2) {
        }

        public override void GraphVizPrinter() {
            base.GraphVizPrinter();
        }

        public override void Accept(AbstractVisitor visitor) {
            visitor.VisitChild2(this);
        }
    }


    class Program {
        static void Main(string[] args) {
            Child1 root = new Child1();
            Child1 n1 = new Child1();
            Child2 n2 = new Child2();

            root.AddChild(n1);
            root.AddChild(n2);

            root.GraphVizPrinter();
            
            GraphVizPrinterVisitor STprinter = new GraphVizPrinterVisitor();
            STprinter.VisitChild1(root);


        }
    }
}
