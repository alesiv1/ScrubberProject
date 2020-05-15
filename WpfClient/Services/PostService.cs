using GraphQL.Client;
using GraphQL.Common.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfClient.Model;

namespace WpfClient.Services
{
	public class PostService
	{
        protected readonly GraphQLClient _client;
        private readonly string _url = "http://localhost:3835/graphql";

        public PostService()
        {
            _client = new GraphQLClient(_url);
        }

        public List<Post> GetAllPost()
        {
            var request = new GraphQLRequest()
            {
                Query = @"
                    query { 
                        posts { 
                            id,
                            title,
                            author,
                            url,
                            description,
                            date,
                            type,
                            topics,
                            numberOfViews,
                            like
                        }
                    }"
            };
            var graphQLResponse = _client.PostAsync(request).Result;
            var posts = graphQLResponse.GetDataFieldAs<List<Post>>("posts");
            return posts;
        }

        public async Task<bool> FeedbackAsync(int id, bool like)
        {
            var request = new GraphQLRequest()
            {
                Query = @"
                    mutation ($like: Boolean!, $id:ID!)
                      {
                        addLike(like:$like, id:$id)
                            { 
                               like,
                               id
                            }
                     }",
                Variables = new { like = like, id = id }
            };
            var graphQLResponse = await _client.PostAsync(request);
            var upPost= graphQLResponse.GetDataFieldAs<Post>("addLike");
            return upPost.Like;
        }
    }
}
