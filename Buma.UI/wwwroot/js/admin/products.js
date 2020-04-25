var app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loadiing: false,
        objectIndex: 0,
        productModel: {
            id: 0,
            name: 'Product Name',
            description: 'Product Description',
            value: 9.99
        },
        products: []
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct: function (id) {
            this.loading = true;
            axios.get('/products/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                });
        },
        getProducts: function () {
            this.loading = true;
            axios.get('/products')
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
            axios.post('/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                    this.editing = false;
                });
        },
        updateProduct: function () {
            this.loading = true;
            axios.put('/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                    this.editing = false;
                });
        },
        deleteProduct: function (id, index) {
            this.loading = true;
            axios.delete('/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loadiing = true;
                });
        },
        newProduct: function () {
            this.editing = true;
            this.productModel.id = 0;
        },
        editProduct: function (id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;
        },
        cancel: function () {
            this.editing = false;
        }
    },
    computed: {
    }
});