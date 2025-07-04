
import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const fetchProduct = createAsyncThunk('product/fetchProduct',
    async () => {
        const response = await axios.get('http://localhost:8080/products/getAll');
        return response.data;
    }
);

export const addProduct = createAsyncThunk('product/addProduct',
    async (product) => {
        const response = await axios.post('http://localhost:8080/products/add', product);
        return response.status;
    }
);