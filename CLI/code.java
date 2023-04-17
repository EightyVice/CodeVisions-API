public class Link{

    static int fib(int n)
    {
        if (n <= 1)
            return n;
        return fib(n - 1) + fib(n - 2);
    }
 
    public static void Main()
    {
        int n = 6;
        print(fib(n));
    }
}
	/*
		Link a = new Link();
		Link y = new Link();
		Link b = a;
		Link c = null;
		int[] arr = {1,2,3,4};
		b.x = 4;
		func1(1);


                // Find sum of array element
        int sum = 0;
         
        for (int i = 0; i < n; i++)
            sum += a[i];
     
        return (double)sum / n;


		----


	// Function that return average of an array.
   static double average(int[] a, int n)
   {
        // Find sum of array element
        int sum = 0;
         
        for (int i = 0; i < n; i++)
            sum += a[i];

        print(sum/n);
   }
     
    //driver code
    public static void Main()
    {
         
        int[] arr = {10, 2, 3, 4, 5, 6, 7, 8, 9};
        int n = arr.length;
        average(arr,n);
    }

	*/