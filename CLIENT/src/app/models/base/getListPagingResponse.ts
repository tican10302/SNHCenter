export interface GetListPagingResponse<T> {
  pageIndex: number | 0;
  totalRow: number | 0;
  data: T;
}
