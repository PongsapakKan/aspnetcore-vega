import { JwtHelper } from 'angular2-jwt';
// app/auth.service.ts

import { Injectable }      from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';

// Avoid name not found warnings
import Auth0Lock from 'auth0-lock';

@Injectable()
export class Auth {
  private roles: string[] = [];
  profile: any;

  // Configure Auth0
  lock = new Auth0Lock('JgdPTZCQQf0Juwxy92Da9DCOdoK73ROY', 'pepperonis.auth0.com', {});
  
  constructor() {    
    this.profile = JSON.parse(localStorage.getItem('profile'));
    
    const token = localStorage.getItem('token');
    if (token) {
      var jwtHelper = new JwtHelper();
      var decodedToken = jwtHelper.decodeToken(token);
      this.roles = decodedToken['https://pepperonis.com/roles'];
    }
    
    this.lock.on("authenticated", (authResult) => {
        localStorage.setItem('token', authResult.accessToken);

        var jwtHelper = new JwtHelper();
        var decodedToken = jwtHelper.decodeToken(authResult.accessToken);
        this.roles = decodedToken['https://pepperonis.com/roles'];

        this.lock.getUserInfo(authResult.accessToken, (error, profile) => {
          if (error)
            throw error;
            
          localStorage.setItem('profile', JSON.stringify(profile));
          this.profile = profile;
        });
    });
  }

  public isInRole(roleName) {
    return this.roles.indexOf(roleName) > -1;
  }

  public login() {
    // Call the show method to display the widget.
    this.lock.show();
  }

  public authenticated() {
    // Check if there's an unexpired JWT
    // This searches for an item in localStorage with key == 'token'
    return tokenNotExpired('token');
  }

  public logout() {
    // Remove token from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('profile');
    this.profile = null;
    this.roles = [];
  }
}