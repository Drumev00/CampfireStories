import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';
import { AuthService } from './services/auth/auth.service';
import { SharedModule } from './shared/shared.module';
import { AdminModule } from './admin/admin.module';
import { CategoryService } from './services/admin/category.service';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { AdminGuardService } from './services/auth/admin-guard.service';
import { TokenInterceptorService } from './services/auth/token-interceptor.service';
import { UploadService } from './services/upload/upload.service';
import { ErrorInterceptorService } from './services/error/error-interceptor.service';
import { CoreModule } from './core/core.module';
import { StoryService } from './services/story/story.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    UserModule,
    SharedModule,
    AdminModule,
    CoreModule,
    NgbModule,
  ],
  providers: [
    AuthService,
    CategoryService,
    AuthGuardService,
    AdminGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    DatePipe,
    UploadService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true,
    },
    StoryService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
