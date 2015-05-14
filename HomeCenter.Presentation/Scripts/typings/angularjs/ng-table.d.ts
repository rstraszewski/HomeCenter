declare module ngTable {
    class ngTableParams {
        data: any[];

        constructor(parameters: any, settings: any);

        parameters(newParameters: string, parseParamsFromUrl: string): any;

        settings(newSettings: string): any;

        page(page: number): any;
        page(): number;
        total(total: number): any;

        count(count: string): any;

        filter(filter: string): any;

        sorting(sorting: string): any;

        isSortBy(field: string, direction: string): any[];

        orderBy(): string[];

        getData($defer: any, params: any);

        getGroups($defer: any, column: any);

        generatePagesArray(currentPage: boolean, totalItems: boolean, pageSize: boolean): any[];

        url(asString: boolean): any[];

        reload();

        reloadPages();
    }
} 