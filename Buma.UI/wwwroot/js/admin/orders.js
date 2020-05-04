var app = new Vue({
    el: '#app',
    data: {
        status: 0,
        loading: false,
        orders: [],
        selectedOrders: null
    },
    mounted() {
        this.getOrders();
    },
    watch: function () {
        this.getOrders();
    },
    methods: {
        getOrders() {
            this.loading = true;
            axios.get('/orders?status=' + this.status)
                .then(result => {
                    this.orders = result.data;
                    this.loading = false;
                });
        },
        selectOrder(id) {
            this.loading = true;
            axios.get('/orders/' + id)
                .then(result => {
                    this.selectedOrder = result.data;
                    this.loading = false;
                });
        },
        updateOrder(id) {
            this.loading = true;
            axios.put('/orders/' + this.selectedOrders.id, null)
                .then(result => {
                    this.loading = false;
                    this.exitOrder();
                    this.loading = false;
                });
        },
        exitOrder() {
            this.selectedOrder = null;
        },
        computed: {

        }
    }
})