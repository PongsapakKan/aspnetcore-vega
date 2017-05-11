import * as _ from 'underscore';
import { Vehicle } from './../../models/vehicle';
import { SaveVehicle } from '../../models/save-vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastyService } from 'ng2-toasty';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastyService: ToastyService) { 
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
    });
  }

  ngOnInit() {
    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeature(),
    ];

    if (this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id))

    Observable.forkJoin(sources).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];

      if (this.vehicle.id) {
        this.setVehicle(data[2]);
        this.populateModels();
      }
    }, err => {
      if (err.status == 404)
          this.router.navigate(['/home']);
    });
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    let selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(id, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(id);         
    } else {
      let index = this.vehicle.features.indexOf(id);
      this.vehicle.features.splice(index, 1);
    }
  }

  onSubmit() {
    if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle)
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'The vehicle was successfully update.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 3000
        })
      });
    } else {
      this.vehicleService.create(this.vehicle)
        .subscribe(v => {
          this.toastyService.success({
            title: 'Success',
            msg: 'The vehicle was successfully update.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 3000
          })
        });
    }
  }

  onDelete() {
    this.vehicleService.delete(this.vehicle.id)
      .subscribe(x => {
        this.router.navigate(['/home']);
      });
  }
}