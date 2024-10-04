export interface GetListRequestModel {
  search: string | '';
  fromDate: Date | null;
  toDate: Date | null;
  offset: number | 0;
  limit: number | 10;
  order: string | null;
  sort: string | null;
}

export function CreateDefaultGetListPagingRequest(): GetListRequestModel {
  return {
    search: '',
    fromDate: null,
    toDate: null,
    offset: 0,
    limit: 10,
    order: null,
    sort: null,
  }
}
