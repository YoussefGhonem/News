export interface Pagination {
        // PaginationHeaderالحجات اللي موجوده ف ال 
        currentPage : number;
        itemsPerPage : number;
        totalItems : number;
        totalPages :number;
}

export class PaginationResult<T> {
    //اللي بتيجي items دي ال 
    result : T; // T: معناها ان دي قايمه من اي نوع 
    pagination :Pagination;// Headerاللي ف ال  Pagination دي المعلومات الخاصه بال 
}