import * as _ from 'underscore';
import { Router } from '@angular/router';
import { Vehicle } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  filter = {
    makeId: 0
  };
  query: any = {

  };
  columns = [
    { title: 'Id'},
    { title: 'Make', key: 'make', isSortable: true},
    { title: 'Model', key: 'model', isSortable: true},
    { title: 'Contact', key: 'contact', isSortable: true},
    { title: 'View'}
  ]
  makes: any[];
  vehicles: Vehicle[];
  showVehicles: Vehicle[];
  constructor(
    private vehicleService: VehicleService,
    private router: Router
  ) { }

  ngOnInit() {
      this.vehicleService.getMakes()
        .subscribe(res => this.makes = res);
      
      this.populateVehicles();
  }

  private populateVehicles() {
      this.vehicleService.getVehicles()
        .subscribe(res => {
          this.vehicles = res;
          this.showVehicles = res;
        });           
  }

  onChangeMake() {
    if (!this.filter.makeId) {
      this.showVehicles = this.vehicles;            
    } else {
      this.showVehicles = _.filter(this.vehicles,
        (vehicles) => {
          return vehicles.make.id == this.filter.makeId;
      });
    }   
  }

  sortBy(column) {
    if (this.query.sortBy === column) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {      
      this.query.sortBy = column;
      this.query.isSortAscending = true;
    }
  }

}
