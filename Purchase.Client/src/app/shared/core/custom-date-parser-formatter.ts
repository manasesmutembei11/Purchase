import { Inject, Injectable } from '@angular/core';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
  NgbTimeStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { padNumber } from '@ng-bootstrap/ng-bootstrap/util/util';
import { format, parse, parseISO } from 'date-fns';
const pad = (i: number): string => i < 10 ? `0${i}` : `${i}`;
@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {

  constructor(@Inject('DATE_FORMAT') private formatTemplate: string){
    super();
  }
  parse(value: string): NgbDateStruct | null {
    let refdata: number = 0;
    let date = parse(value, this.formatTemplate, refdata);


    return {
      day: date.getDate(),
      month: date.getMonth()+1,
      year: date.getFullYear(),
    };
  }

  format(date: NgbDateStruct | null): string {
    if (date === null) {
      return '';
    }
    return format(new Date(date.year, date.month - 1, date.day), this.formatTemplate);
  }
}
// export function fromDatePicker(date: NgbDateStruct | any): Date | any {
//   if(!date) return null
//   return new Date(date.year, date.month - 1, date.day);
// }
// export function fromTimePicker(time: NgbTimeStruct): string|null {
//   return time != null ? `${pad(time.hour)}:${pad(time.minute)}:${pad(time.second)}` : null;
// }

// export function fromModelToTimePicker(value: string| null): NgbTimeStruct | null {
//   if (!value) {
//     return null;
//   }
//   const split = value.split(':');
//   return {
//     hour: parseInt(split[0], 10),
//     minute: parseInt(split[1], 10),
//     second: parseInt(split[2], 10)
//   };
// }


// export function fromISOToDatePicker(date: string |any) : NgbDateStruct | any {

//   if (date) {
//     let iso = parseISO(date);
//     if (iso) {
//      let pickerdate:NgbDateStruct= {
//         day: iso.getDate(),
//         month: iso.getMonth()+1,
//         year: iso.getFullYear(),
//       };
//       return pickerdate
//     }
//   }
//   return null;
// }


