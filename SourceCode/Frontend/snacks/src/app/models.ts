export class PagedResponse<TModel> {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
    public model: TModel[];
    public pageSize: number;
    public pageNumber: number;
    public itemsCount: number;
    public pageCount: number;
}

export class Product {
    public productID: number;
    public productName: string;
    public productDescription: string;
}
