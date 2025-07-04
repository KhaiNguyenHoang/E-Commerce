import {configureStore} from "@reduxjs/toolkit";
import productReducer from "./productSlice";
const productStore = configureStore({
    reducer: {
        product: productReducer
    }
})

export default productStore;