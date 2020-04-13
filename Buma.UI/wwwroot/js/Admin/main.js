var app = new Vue({
    el: '#app',
    data: {
        loadiing: false,
        productModel: {
            name: 'Product Name',
            description: 'Product Description',
            value: 9.99
        },
        products: []
    },
    methods: {
        getProducts: function () {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                });
        },
        createProduct: function () {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                });
        }
    },
    computed: {
    }
});