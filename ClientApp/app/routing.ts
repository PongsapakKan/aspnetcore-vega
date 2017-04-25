import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';

const routing = RouterModule.forRoot([
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'vehicle/new', component: VehicleFormComponent },
    { path: '**', redirectTo: 'home' }
]);

export default routing;