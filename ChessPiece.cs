// Fig. 16.25 : ChessPiece.cs
// Storage class for chess piece attributes.

using System;
using System.Drawing;

namespace Chess
{
   /// <summary>
   /// represents a chess piece
   /// </summary>
   public class ChessPiece
   {
      // define chess-piece type constants
      public enum Types
      {
         KING,
         QUEEN,
         BISHOP,
         KNIGHT,
         ROOK,
         PAWN
      }

      private int currentType; // this object's type
      private Bitmap pieceImage; // this object's image

      // default display location
      private Rectangle targetRectangle = 
         new Rectangle( 0, 0, 75, 75 );

      // construct piece
      public ChessPiece( int type, int xLocation, 
         int yLocation, Bitmap sourceImage )
      {
         currentType = type; // set current type
         targetRectangle.X = xLocation; // set current x location
         targetRectangle.Y = yLocation; // set current y location

         // obtain pieceImage from section of sourceImage
         pieceImage = sourceImage.Clone( 
            new Rectangle( type * 75, 0, 75, 75 ), 
            System.Drawing.Imaging.PixelFormat.DontCare );
      }

      // draw chess piece
      public void Draw( Graphics graphicsObject )
      {
         graphicsObject.DrawImage( pieceImage, targetRectangle );

      } 

      // obtain this piece's location rectangle
      public Rectangle GetBounds()
      {
         return targetRectangle;

      } // end method GetBounds

      // set this piece's location
      public void SetLocation( int xLocation, int yLocation )
      {
         targetRectangle.X = xLocation;
         targetRectangle.Y = yLocation;

      } // end method SetLocation

   } // end class ChessPiece
}

/*
 **************************************************************************
 * (C) Copyright 2002 by Deitel & Associates, Inc. and Prentice Hall.     *
 * All Rights Reserved.                                                   *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 **************************************************************************
*/