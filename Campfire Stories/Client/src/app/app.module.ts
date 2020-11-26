import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';
import { AuthService } from './services/auth/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { AdminModule } from './admin/admin.module';
import { CategoryService } from './services/admin/category.service';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { AdminGuardService } from './services/auth/admin-guard.service';
import { TokenInterceptorService } from './services/auth/token-interceptor.service';
import { DatePipe } from '@angular/common';
import { UploadService } from './services/upload/upload.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    UserModule,
    SharedModule,
    AdminModule
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
    UploadService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
