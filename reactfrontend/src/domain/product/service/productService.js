import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";

export const fetchProduct = async () => createAsyncThunk('product/fetchProduct',
    async () => {
    let response = axios.get('http://localhost:8080/products/getAll');
    return (await response).data;
})

export const addProduct = async (product) => createAsyncThunk('product/addProduct',
    async () => {
    let response = axios.post('http://localhost:8080/products/add', product);
    return (await response).status;
})