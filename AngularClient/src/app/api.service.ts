import { Injectable } from '@angular/core';
import gql from 'graphql-tag';
import { Apollo } from 'apollo-angular';

export interface CreateLikeMutationResponse {
  updInfo: any;
  loading: boolean;
}

const CREATE_LIKE_MUTATION = gql`
  mutation($like: Boolean!, $id:ID!) {
    addLike(like:$like, id:$id) {
       like,
       id
    }
  }
`;

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private apollo: Apollo) {}

  getAllNews(): any {}

  updateNews(id: number, like: boolean): any {
    this.apollo
      .mutate<CreateLikeMutationResponse>({
        mutation: CREATE_LIKE_MUTATION,
        variables: {
          like,
          id,
        },
      })
      .subscribe((response) => {});
  }
}
