export class GetListRequestModel {
  search: string | '' = '';
  fromDate: Date | null = null;
  toDate: Date | null = null;
  offset: number = 0;
  limit: number = 10;
  order: string | null = null;
  sort: string | null = null;
}

