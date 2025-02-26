# Client

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.2.3.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.

npm install ngx-toastr
ng g guard _guards/auth --dry-run
ng g guard _guards/auth --skip-tests
npm i bootswatch
ng g c messages --skip-tests
ng g c lists --skip-tests
ng g c members/member-detail --skip-tests
ng g c members/member-list --skip-tests
ng g c errors/test-errors --skip-tests
ng g interceptor _interceptors/error --skip-tests
>ng g c errors/not-found --skip-tests
>ng g c errors/server-error --skip-tests
https://json-generator.com/
[
  '{{repeat(5, 7)}}',
  {
    UserName: '{{firstName("female")}}',
    gender: 'female',
    DateOfBirth:'{{date(new Date(1970, 0, 1), new Date(2000, 11, 31), "YYYY-MM-dd")}}',
    KnownAs: function(){return this.UserName; },
    Created: '{{date(new Date(2025, 0, 1), new Date(), "YYYY-MM-dd")}}',
    LastActive: '{{date(new Date(2025, 1, 1), new Date(), "YYYY-MM-dd")}}',
    Introduction: '{{lorem(1, "paragraphs")}}',
    LookingFor: '{{lorem(1, "paragraphs")}}',
    Interests: '{{lorem(1, "sentences")}}',
    City: '{{city()}}',
    Country: '{{country()}}',
    Photos: [
      {
        PhotoUrl: function(num){
          return 'https://randomuser.me/api/portraits/women/' + num.integer(1, 99) + '.jpg';    
        },
        IsMain: true
      }
    ]
  }
]

[
  '{{repeat(5, 7)}}',
  {
    UserName: '{{firstName("male")}}',
    gender: 'male',
    DateOfBirth:'{{date(new Date(1970, 0, 1), new Date(2000, 11, 31), "YYYY-MM-dd")}}',
    KnownAs: function(){return this.UserName; },
    Created: '{{date(new Date(2025, 0, 1), new Date(), "YYYY-MM-dd")}}',
    LastActive: '{{date(new Date(2025, 1, 1), new Date(), "YYYY-MM-dd")}}',
    Introduction: '{{lorem(1, "paragraphs")}}',
    LookingFor: '{{lorem(1, "paragraphs")}}',
    Interests: '{{lorem(1, "sentences")}}',
    City: '{{city()}}',
    Country: '{{country()}}',
    Photos: [
      {
        PhotoUrl: function(num){
          return 'https://randomuser.me/api/portraits/men/' + num.integer(1, 99) + '.jpg';    
        },
        IsMain: true
      }
    ]
  }
]
dotnet ef migrations add UpdatedUserEntity
dotnet ef database drop
