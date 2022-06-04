namespace Txtai
{
    /// <summary>
    /// The TxtAI API.
    /// Represents the index result of a text query.
    /// Common object.
    /// </summary>
    public struct IndexResult
    {
        public int id;
        public double score;

        /// <summary>
        /// Initializes a new instance of the <see cref="Txtai.IndexResult"/> struct.
        /// </summary>
        public IndexResult(int id, double score)
        {
            this.id = id;
            this.score = score;
        }
    }
}
