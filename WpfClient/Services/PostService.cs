using GraphQL.Client;
using GraphQL.Common.Request;
using System;
using System.Collections.Generic;
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

        public bool Feedback(Post post, bool like)
        {
            int id = post.Id;
            post.Like = like;
            var request = new GraphQLRequest()
            {
                Query = @"
                    mutation ($post: PostInfoInput!, $id:ID!)
                      {
                        addLike(post:$post, id:$id)
                            { 
                               name                            
                            }
                     }",
                Variables = new { info = post, infoId = id }
            };
            var graphQLResponse = _client.PostAsync(request).Result;
            var upPost= graphQLResponse.GetDataFieldAs<Post>("addLike");
            return upPost.Like;
        }
    }
}
