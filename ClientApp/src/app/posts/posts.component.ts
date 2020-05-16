import { Component, OnInit, NgModule } from '@angular/core';
import { ApiService } from '../api.service';
import gql from 'graphql-tag';
import { Apollo } from 'apollo-angular';

@Component({
  selector: 'app-news-component',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
})
export class PostsComponent implements OnInit {
  posts: any[];
  loading = true;
  error: any;
  updatedPosts: any;
  id: any;

  constructor(private apollo: Apollo, private api: ApiService) {}

  updatePosts(isLiked: boolean, info: any) {
    this.updatedPosts = info;
    this.updatedPosts.like = isLiked;
    this.api
      .updateNews(this.id, this.updatedPosts.like)
      .subscribe((response) => {});
  }

  ngOnInit() {
    this.apollo
      .query<any>({
        query: gql`
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
          }
        `,
      })
      .subscribe(
        ({ data, loading }) => {
          this.posts = data && data.getAllNews;
          this.loading = loading;
        },
        (error) => {
          this.loading = false;
          this.error = error;
        }
      );
  }
}
