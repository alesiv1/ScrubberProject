import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PostsComponent } from './posts/posts.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GraphQLModule } from './graphql.module';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material.module';

@NgModule({
  declarations: [AppComponent, PostsComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    GraphQLModule,
    HttpClientModule,
    MaterialModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
