import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';


@Injectable({
  providedIn: 'root'
})
export class MessageBoxService {

  constructor() { }

  confirm(message:string){
    return  Swal.fire({
      title: "Confirm",
      text: message,
      icon: 'warning',
      showCancelButton: true,
      allowOutsideClick: false,
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',

    })

  }
  alert(message:string){
    return  Swal.fire({
      title: "Warning",
      text: message,
      icon: 'warning',   

    })  



  }
  success(message:string){
    return  Swal.fire({
      title: "Success",
      text: message,
      icon: 'success',   

    })  

   
  }
  toastSuccess(message:string){
    Swal.fire({
      position: 'top',
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500,
      toast:true
    })

  }
  toastWarning(message:string){
    Swal.fire({
      position: 'top',
      icon: 'warning',
      title: message,
      imageHeight:30,
      showConfirmButton: false,
      timer: 1500,
      toast:true
    })
  }




}
