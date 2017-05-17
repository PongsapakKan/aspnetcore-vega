import { NgZone } from '@angular/core';
import { ProgressService } from './../../services/progress.service';
import { PhotoService } from './../../services/photo.service';
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, Params } from "@angular/router";

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos: any[];
  progress: any;

  constructor(private zone: NgZone,
              private route: ActivatedRoute,
              private router: Router,
              private toasty: ToastyService,
              private progressSerivce: ProgressService,
              private photoService: PhotoService,
              private vehicleService: VehicleService) { 

    route.params.subscribe((params: Params) => {
      this.vehicleId = +params['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        this.router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {  
    this.photoService.getPhotos(this.vehicleId)
      .subscribe(photos => {
        this.photos = photos
      },
      err => {
        this.toasty.error({
          title: 'Error',
          msg: err.text(),
          theme: 'bootstrap',
          showClose: true,
          timeout: 3000
        });
      });

    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
      v => this.vehicle = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return;
        }
      }
      );
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  uploadPhoto() {
    var nativeElement: HTMLInputElement =  this.fileInput.nativeElement;

    this.progressSerivce.startTracking()
      .subscribe(progress => {
        this.zone.run(() => {
          console.log(progress);
          this.progress = progress;
        });
      },
      null,
      () => {
        this.progress = null;
      });

    this.photoService.upload(this.vehicleId, nativeElement.files[0])
      .subscribe(photo => {
        this.photos.push(photo);
      });
  }
}
