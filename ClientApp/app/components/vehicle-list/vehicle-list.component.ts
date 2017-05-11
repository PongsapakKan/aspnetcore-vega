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
  query: any = {};
  columns = [
    { title: 'Id'},
    { title: 'Make', key: 'make', isSortable: true},
    { title: 'Model', key: 'model', isSortable: true},
    { title: 'Contact', key: 'contactName', isSortable: true},
    { title: 'View'}
  ]
  makes: any[];
  models: any[];
  // allVehicles: Vehicle[];
  vehicles: Vehicle[];
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
    this.vehicleService.getVehicles(this.query)
      .subscribe(res => {
        // this.allVehicles = res;
        this.vehicles = res;
      });           
  }

  onFilterMakeChange() {
    // var vehicles = this.allVehicles;
    // if (this.filter.makeId) 
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
    
    // if (this.filter.modelId)
    //   vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);

    // this.vehicles = vehicles;
    let selectedMake = this.makes.find(m => m.id == this.query.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    this.populateVehicles();
  }

  onFilterModelChange() {
    this.populateVehicles();
  }

  resetFilter() {
    this.query = {};
    this.onFilterMakeChange();
  }

  sortBy(column) {
    if (!column)
      return;
      
    if (this.query.sortBy === column) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {      
      this.query.sortBy = column;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

}
