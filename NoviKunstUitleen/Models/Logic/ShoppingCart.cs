using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoviKunstUitleen.Models;
using NoviKunstUitleen.ViewModel;

namespace NoviKunstUitleen.Models
{

    //The ShoppingCart model handles data access to the Cart table. 
    //Additionally, it will handle the business logic to for adding and removing items from the shopping cart.

    public partial class ShoppingCart
    {
        private ApplicationDbContext _context;
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartIds";

        public ShoppingCart()
        {
            _context = new ApplicationDbContext();
        }
        protected void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }


        public void AddToCart(Arts art, string endOfLoan)
        {
            // Get the matching cart and art instances
            var cartItem = _context.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ArtsId == art.ArtsId);

            var ArtinDB = _context.Arts.Single(a => a.ArtsId == art.ArtsId);
            ArtinDB.NumbersAvailable -= 1;
            _context.SaveChanges();
            if (cartItem == null)
            {
            // Create a new cart item if no cart item exists
            cartItem = new Cart
            {
                ArtsId = art.ArtsId,
                CartId = ShoppingCartId,
                Count = 1,
                DateCreated = DateTime.Now,
                EndOfLoan = endOfLoan
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _context.SaveChanges();
        }
        public int RemoveFromCart(int id, string artName)
        {
            // Get the cart
            var cartItem = _context.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

        var ArtinDB = _context.Arts.Single(a => a.Name == artName);
        ArtinDB.NumbersAvailable += 1;
        _context.SaveChanges();

        int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }
                // Save changes
                _context.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _context.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return _context.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.Carts
                            where cartItems.CartId == ShoppingCartId
                            select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply art price by count of that art to get 
            // the current price for each of those Art in the cart
            // sum all art price totals to get the cart total
            decimal? total = (from cartItems in _context.Carts
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.Count *
                                cartItems.Arts.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
                
            decimal orderTotal = 0;
                
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
            var orderDetail = new OrderDetail
            {
                ArtsId = item.ArtsId,
                OrderId = order.OrderId,
                UnitPrice = item.Arts.Price,
                Quantity = item.Count,
                EndOfLoan = item.EndOfLoan
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Arts.Price);

                _context.OrderDetails.Add(orderDetail);

            }
        // Set the order's total to the orderTotal count
        order.Total = orderTotal;

        _context.Orders.Add(order);
        // Save the order
        _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }


    // We're using HttpContextBase to allow access to cookies.
    public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
        }
    }
   
}