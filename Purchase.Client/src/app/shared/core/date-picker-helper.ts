import { NgbDateStruct, NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { parseISO } from "date-fns";
const pad = (i: number): string => i < 10 ? `0${i}` : `${i}`;
export class DpHelper {

  static fromISO(date: string | any): NgbDateStruct | any {
    if (date) {
      let iso = parseISO(date);
      if (iso) {
        let pickerdate: NgbDateStruct = {
          day: iso.getDate(),
          month: iso.getMonth() + 1,
          year: iso.getFullYear(),
        };
        return pickerdate
      }
    }
    return null;
  }

  static toDate(date: NgbDateStruct | any): Date | any {
    if (!date) return null
    return new Date(date.year, date.month - 1, date.day);
  }
  static toISODate(date: NgbDateStruct | any): string | any {
    if (!date) return null
    const jsDate = new Date();
    jsDate.setUTCFullYear(date.year);
    jsDate.setUTCMonth(date.month - 1);
    jsDate.setUTCDate(date.day); 
    return jsDate.toISOString();
  }


}
export class DtHelper {

  static toTimespan(time: NgbTimeStruct): string | null {
    return time != null ? `${pad(time.hour)}:${pad(time.minute)}:${pad(time.second)}` : null;
  }
  fromString(value: string | null): NgbTimeStruct | null {
    if (!value) {
      return null;
    }
    const split = value.split(':');
    return {
      hour: parseInt(split[0], 10),
      minute: parseInt(split[1], 10),
      second: parseInt(split[2], 10)
    };
  }
}