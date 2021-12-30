using System;
using System.Collections.Generic;

// https://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
// 
// I'm using an unsorted array for this example, but ideally this
// would be a binary heap. There's an open issue for adding a binary
// heap to the standard C# library: https://github.com/dotnet/corefx/issues/574
//
// Until then, find a binary heap class:
// * https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
// * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
// * http://xfleury.github.io/graphsearch.html
// * http://stackoverflow.com/questions/102398/priority-queue-in-net
namespace GS.Helpers
{
    public class PriorityQueue<T> {
        private List<Tuple<T, double>> _elements = new List<Tuple<T, double>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, double priority) {
            _elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue() {
            int bestIndex = 0;

            for (int i = 0; i < _elements.Count; i++) {
                if (_elements[i].Item2 < _elements[bestIndex].Item2) {
                    bestIndex = i;
                }
            }

            T bestItem = _elements[bestIndex].Item1;
            _elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}
