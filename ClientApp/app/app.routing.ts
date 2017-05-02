import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';

const routing = RouterModule.forRoot([
    { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'vehicles', component: VehicleListComponent },
    { path: 'vehicle/new', component: VehicleFormComponent },
    { path: 'vehicle/:id', component: VehicleFormComponent },
    { path: '**', redirectTo: 'home' }
]);

export default routing;