export interface GetListPagingRequest {
  search: string | '';
  fromDate: Date | null;
  toDate: Date | null;
  offset: number | 0;
  limit: number | 10;
  order: string | null;
  sort: string | null;
}
