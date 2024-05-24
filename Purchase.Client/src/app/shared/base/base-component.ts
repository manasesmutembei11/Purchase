import { TitleItem } from "../../core/models/shared-models/bread-crum-item";

export class BaseComponent {
  breadCrumbItems: Array<TitleItem> = [];
  errors!: any[];
  pageTitle: string = '';

  addbcItem(text: string, active?: boolean) {
   text= text.toLowerCase().replace(/(^\w|\s\w)/g, m => m.toUpperCase());
    this.breadCrumbItems.push({
      label: text,
      active:active
    })
  }


}