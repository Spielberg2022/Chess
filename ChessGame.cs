// Fig. 16.26: ChessGame.cs
// Chess Game graphics code.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Chess
{
   /// <summary>
   /// allows 2 players to play chess
   /// </summary>
   public class ChessGame : System.Windows.Forms.Form
   {
      private System.Windows.Forms.PictureBox pieceBox;
      private System.Windows.Forms.MainMenu GameMenu;
      private System.Windows.Forms.MenuItem gameItem;
      private System.Windows.Forms.MenuItem newGameItem;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private 
         System.ComponentModel.Container components = null;

      // ArrayList for board tile images
      ArrayList chessTile = new ArrayList();

      // ArrayList for chess pieces
      ArrayList chessPieces = new ArrayList();

      // define index for selected piece
      int selectedIndex = -1;
      int[,] board = new int[ 8, 8 ]; // board array

      // define chess tile size in pixels
      private const int TILESIZE = 75;

      public ChessGame()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if( disposing )
         {
            if (components != null) 
            {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }

		#region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.pieceBox = new System.Windows.Forms.PictureBox();
         this.GameMenu = new System.Windows.Forms.MainMenu();
         this.gameItem = new System.Windows.Forms.MenuItem();
         this.newGameItem = new System.Windows.Forms.MenuItem();
         this.SuspendLayout();
         // 
         // pieceBox
         // 
         this.pieceBox.BackColor = System.Drawing.Color.Transparent;
         this.pieceBox.Name = "pieceBox";
         this.pieceBox.Size = new System.Drawing.Size(600, 600);
         this.pieceBox.TabIndex = 1;
         this.pieceBox.TabStop = false;
         this.pieceBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pieceBox_Paint);
         this.pieceBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseUp);
         this.pieceBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseMove);
         this.pieceBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseDown);
         // 
         // GameMenu
         // 
         this.GameMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.gameItem});
         // 
         // gameItem
         // 
         this.gameItem.Index = 0;
         this.gameItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.newGameItem});
         this.gameItem.Text = "Game";
         // 
         // newGameItem
         // 
         this.newGameItem.Index = 0;
         this.newGameItem.Shortcut = System.Windows.Forms.Shortcut.F2;
         this.newGameItem.Text = "New Game";
         this.newGameItem.Click += new System.EventHandler(this.newGameItem_Click);
         // 
         // ChessGame
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(602, 595);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.pieceBox});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Menu = this.GameMenu;
         this.Name = "ChessGame";
         this.Text = "Chess";
         this.Load += new System.EventHandler(this.ChessGame_Load);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessGame_Paint);
         this.ResumeLayout(false);

      }
		#endregion

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main() 
      {
         Application.Run( new ChessGame() );
      }

      // load tile bitmaps and reset game
      private void ChessGame_Load(
         object sender, System.EventArgs e)
      {
         // load chess board tiles
         chessTile.Add( Bitmap.FromFile( "lightTile1.png" ) );
         chessTile.Add( Bitmap.FromFile( "lightTile2.png" ) );
         chessTile.Add( Bitmap.FromFile( "darkTile1.png" ) );
         chessTile.Add( Bitmap.FromFile( "darkTile2.png" ) );

         ResetBoard(); // initialize board
         Invalidate(); // refresh form
      
      } // end method ChessGame_Load

      // initialize pieces to start and rebuild board
      private void ResetBoard()
      {
         int current = -1;
         ChessPiece piece;
         Random random = new Random();
         bool light = false;
         int type;

         // ensure empty arraylist
         chessPieces = new ArrayList();

         // load whitepieces image
         Bitmap whitePieces = 
            ( Bitmap )Image.FromFile( "whitePieces.png" );

         // load blackpieces image
         Bitmap blackPieces = 
            ( Bitmap )Image.FromFile( "blackPieces.png" );

         // set whitepieces drawn first
         Bitmap selected = whitePieces;

         // traverse board rows in outer loop
         for ( int row = 0; 
            row <= board.GetUpperBound( 0 ); row++ )
         {
            // if at bottom rows, set to black pieces images
            if ( row > 5 )
               selected = blackPieces;

            // traverse board columns in inner loop
            for ( int column = 0; 
               column <= board.GetUpperBound( 1 ); column++ )
            {
               // if first or last row, organize pieces
               if ( row == 0 || row == 7 )
               {
                  switch( column )
                  {
                     case 0:
                     case 7: // set current piece to rook
                        current = 
                           ( int )ChessPiece.Types.ROOK;
                        break;

                     case 1:
                     case 6: // set current piece to knight
                        current = 
                           ( int )ChessPiece.Types.KNIGHT;
                        break;

                     case 2:
                     case 5: // set current piece to bishop
                        current = 
                           ( int )ChessPiece.Types.BISHOP;
                        break;

                     case 3: // set current piece to king
                        current = 
                           ( int )ChessPiece.Types.KING;
                        break;

                     case 4: // set current piece to queen
                        current = 
                           ( int )ChessPiece.Types.QUEEN;
                        break;
                  }

                  // create current piece at start position
                  piece = new ChessPiece( current, 
                     column * TILESIZE, row * TILESIZE, 
                     selected );

                  // add piece to arraylist
                  chessPieces.Add( piece );
               }

               // if second or seventh row, organize pawns
               if ( row == 1 || row == 6 )
               {
                  piece = new ChessPiece( 
                     ( int )ChessPiece.Types.PAWN,
                     column * TILESIZE, row * TILESIZE, 
                     selected );

                  // add piece to arraylist
                  chessPieces.Add( piece );
               }

               // determine board piece type
               type = random.Next( 0, 2 );

               if ( light )
               {
                  // set light tile
                  board[ row, column ] = type;
                  light = false;
               }
               else
               {
                  // set dark tile
                  board[ row, column ] = type + 2;
                  light = true;
               }
            }

            // account for new row tile color switch
            light = !light;
         }

      } // end method ResetBoard

      // display board in form OnPaint event
      private void ChessGame_Paint(
         object sender, System.Windows.Forms.PaintEventArgs e)
      {
         // obtain graphics object
         Graphics graphicsObject = e.Graphics;

         for ( int row = 0; 
            row <= board.GetUpperBound( 0 ); row++ )
         {
            for ( int column = 0; 
               column <= board.GetUpperBound( 1 ); column++ )
            {
               // draw image specified in board array
               graphicsObject.DrawImage(
                  (Image)chessTile[ board[ row, column ] ],
                  new Point( TILESIZE * column,
                  TILESIZE * row ) );
            }
         }
      
      } // end method ChessGame_Paint

      // return index of piece that intersects point
      // optionally exclude a value
      private int CheckBounds( Point point, int exclude )
      {
         Rectangle rectangle; // current bounding rectangle

         for ( int i = 0; i < chessPieces.Count; i++ )
         {
            // get piece rectangle
            rectangle = GetPiece( i ).GetBounds();

            // check if rectangle contains point
            if ( rectangle.Contains( point ) && i != exclude )
               return i;
         }
         return -1;

      } // end method CheckBounds

      // handle pieceBox paint event
      private void pieceBox_Paint(
         object sender, System.Windows.Forms.PaintEventArgs e)
      {
         // draw all pieces
         for ( int i = 0; i < chessPieces.Count; i++ )
            GetPiece( i ).Draw( e.Graphics );
      
      } // end method pieceBox_Paint

      private void pieceBox_MouseDown(
         object sender, System.Windows.Forms.MouseEventArgs e)
      {
         // determine selected piece
         selectedIndex = 
            CheckBounds( new Point( e.X, e.Y ), -1 );
      
      } // end method pieceBox_MouseDown

      // if piece is selected, move it
      private void pieceBox_MouseMove(
         object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if ( selectedIndex > -1 )
         {
            Rectangle region = new Rectangle( 
               e.X - TILESIZE * 2, e.Y - TILESIZE * 2, 
               TILESIZE * 4, TILESIZE * 4 );

            // set piece center to mouse
            GetPiece( selectedIndex ).SetLocation( 
               e.X - TILESIZE / 2, e.Y - TILESIZE / 2 );

            // refresh immediate are
            pieceBox.Invalidate( region );
         }
      
      } // end method pieceBox_MouseMove

      // on mouse up deselect piece and remove taken piece
      private void pieceBox_MouseUp(
         object sender, System.Windows.Forms.MouseEventArgs e)
      {
         int remove = -1;

         //if chess piece was selected
         if ( selectedIndex > -1 )
         {
            Point current = new Point( e.X, e.Y );
            Point newPoint = new Point( 
               current.X - ( current.X % TILESIZE ),
               current.Y - ( current.Y % TILESIZE ) );

            // check bounds with point, exclude selected piece
            remove = CheckBounds( current, selectedIndex );

            // snap piece into center of closest square
            GetPiece( selectedIndex ).SetLocation( newPoint.X,
               newPoint.Y );

            // deselect piece
            selectedIndex = -1;

            // remove taken piece
            if ( remove > -1 )
               chessPieces.RemoveAt( remove );

         }

         // refresh pieceBox to ensure artifact removal
         pieceBox.Invalidate();
      
      } // end method pieceBox_MouseUp

      // helper function to convert 
      // ArrayList object to ChessPiece
      private ChessPiece GetPiece( int i )
      {
         return (ChessPiece)chessPieces[ i ];
      } // end method GetPiece

      // handle NewGame menu option click
      private void newGameItem_Click(
         object sender, System.EventArgs e)
      {
         ResetBoard(); // reinitialize board
         Invalidate(); // refresh form
      
      } // end method newGameItem_Click

   } // end class ChessGame
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