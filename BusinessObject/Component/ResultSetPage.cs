using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessObject.Component
{
    public class ResultSetPage<T> :
     IEnumerable<T>
    {
        #region Fields
        private readonly int offset;
        private readonly int totalCount;
        private readonly IEnumerable<T> results;
        private readonly Lazy<int> count;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ResultSetPage{T}"/>.
        /// </summary>
        /// <param name="offset">The offset of the current page within the complete result set.</param>
        /// <param name="totalCount">The number of results in the complete result set.</param>
        /// <param name="results">The results of the current page.</param>
        public ResultSetPage(
            int offset,
            int totalCount,
            IEnumerable<T> results)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            if (totalCount < offset)
            {
                throw new ArgumentOutOfRangeException("totalCount");
            }

            if (results == null)
            {
                throw new ArgumentNullException("results");
            }

            this.offset = offset;
            this.totalCount = totalCount;
            this.results = results.ToList();
            this.count = new Lazy<int>(() => this.results.Count());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the offset of the current page within the complete result set.
        /// </summary>
        public int Offset
        {
            get { return this.offset; }
        }

        /// <summary>
        /// Gets the number of items in the complete result set.
        /// </summary>
        public int TotalCount
        {
            get { return this.totalCount; }
        }

        /// <summary>
        /// Gets the number of items in the current page.
        /// </summary>
        public int Count
        {
            get { return this.count.Value; }
        }

        /// <summary>
        /// Gets a value indicating whether or not the result set contains additional pages following the one represented by this instance.
        /// </summary>
        public bool HasMorePages
        {
            get { return this.offset + this.Count < this.totalCount; }
        }
        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return this.results.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
