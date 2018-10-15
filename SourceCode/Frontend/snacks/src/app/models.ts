export class PagedResponse<TModel> {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
    public model: TModel[];
}

export class Product {
    public productID: number;
    public productName: string;
    public productDescription: string;
}
