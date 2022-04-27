<script></script>

<ul>
    <li><a class="active" href="#home">Home</a></li>
    <li><a href="#user">User</a></li>
    <li><a href="#product">Product</a></li>
    <li><a href="#product-batch">ProductBatch</a></li>
    <li><a href="#order">Order</a></li>
</ul>

<style>
    ul {
        list-style-type: none;
        margin: 0;
        padding: 10px 0 0 0;
        width: 10%;
        min-width: 150px;
        position: fixed;
        height: 100%;
        overflow: auto;
        background-color: #94d2f9;
    }

    li a {
        color: #000;
        display: block;
        padding: 8px 16px;
        font-weight: bold;
        text-decoration: none;
    }

    li a.active {
        background-color: #4cb5f5;
    }

    li a:hover:not(.active) {
        background-color: #4cb5f5;
    }
</style>