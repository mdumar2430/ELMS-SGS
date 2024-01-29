import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {HttpClientModule } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { JwtModule } from '@auth0/angular-jwt';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), importProvidersFrom(HttpClientModule), provideAnimations(),importProvidersFrom(JwtModule.forRoot({
    config:{
      tokenGetter:()=>{
        return sessionStorage.getItem("jwt");
      },
      allowedDomains:['localhost:7034']
    }
  }))]
};
