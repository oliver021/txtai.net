using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Txtai{

    /// <summary>
    /// The question object that is used to query the TxtAI API.
    /// </summary>
    /// <property name="name">The question name id.</property>
    /// <property name="query"></property>
    /// <property name="filtering">The question name id.</property>
    /// <property name="snippet">The question name id.</property>
    public class Question {

        public string Name { get; set; }
        public string Query { get; set; }
        public string QuestionId { get; set; }
        public bool Snippet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Txtai.Question"/> class.
        /// </summary>
        public Question(string name, string query, string question, bool snippet) {
            this.Name = name;
            this.Query = query;
            this.QuestionId = question;
            this.Snippet = snippet;
        }
    }

    /// <summary>
    /// The extractor answers object that is used to response.
    /// </summary>
    /// <property name="name">The question name.</property>
    /// <property name="answer">The question answer.</property>
    public class Answer {

        /// <summary>
        /// The name of the answer.
        public string name { get; set; }

        /// <summary>
        /// The answer text.
        /// </summary>
        public string AnswerQuestion { get; set; }

        /**
         * Creates an answer.
         * 
         * @param name question identifier/name
         * @param answer answer to question
         */
        public Answer(String name, String answer) {
            this.name = name;
            this.AnswerQuestion = answer;
        }

        /**
         * Answer as String.
         */
        public String toString() {
            return this.name + " " + this.AnswerQuestion;
        }
    }

    public class Extractor : RequestBase
    {
        public Extractor(HttpClient client, IApiSerializer serializer) : base(client, serializer)
        {
        }

        /// <summary>
        /// Extracts answers to input questions.
        /// </summary>
        /// <param name="queue">list of <see cref="Question"/> </param>
        /// <param name="texts">list of text.</param>
        /// <returns>list of <see cref="Answer"/></returns>
        public async Task<List<Answer>> Extract(List<Question> queue, List<string> texts) {
            var body = new {
                queue,
                texts
            };
            var json = this.serializer.Serialize(body);
            var response = await this.client.PostAsync("/extract", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var answers = this.serializer.Deserialize<List<Answer>>(jsonResponse);
            return answers;
        }
    }
}