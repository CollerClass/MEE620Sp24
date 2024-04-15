//============================================================================
// DatGrid.cs
//============================================================================
using Godot;
using System;

public class DatGrid{
    Container parent;
    GridContainer grid;

    int nRow;    // number of rows
    int nCol;    // number of columns

    Label[] rLabels;      // row labels;
    Label[] cLabels;      // column labels;
    Label cornerLabel;    // label in top left corner
    Label[,] vals;        // values

    String[,] fStrings;    // format strings
    String[,] decStrings;  // decimal strings
    String[,] sfxStrings;  // suffix strings

    bool initialized;


    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public DatGrid(Container pContainer)
    {
        parent = pContainer;

        grid = new GridContainer();

        initialized = false;
    }

    //------------------------------------------------------------------------
    // SetGridSize:
    //------------------------------------------------------------------------
    public void SetGridSize(int _nRow, int _nCol)
    {
        if(initialized)
            return;

        if(_nRow < 1 || _nCol < 1)
            return;

        nRow = _nRow;
        nCol = _nCol;

        grid.Columns = 2*nCol + 1;

        int i,j;
        cLabels = new Label[nCol];
        for(i=0;i<nCol;++i){
            cLabels[i] = new Label();
            cLabels[i].Text = "cLabel";
        }
        rLabels = new Label[nRow];
        for(i=0;i<nRow;++i){
            rLabels[i] = new Label();
            rLabels[i].Text = "rLabel";
        }
        cornerLabel = new Label();

        vals = new Label[nRow,nCol];
        fStrings = new string[nRow,nCol];
        decStrings = new string[nRow,nCol];
        sfxStrings = new string[nRow,nCol];
        for(i=0;i<nRow;++i){
            for(j=0;j<nCol;++j){
                vals[i,j] = new Label();
                vals[i,j].Text = "val";
                fStrings[i,j] = "0.00";
                decStrings[i,j] = "0.00";
                sfxStrings[i,j] = "";
            }
        }

        grid.AddChild(cornerLabel);
        for(i=0;i<nCol;++i){
            grid.AddChild(new VSeparator());
            grid.AddChild(cLabels[i]);
        }
        for(i=0;i<nRow;++i){
            grid.AddChild(rLabels[i]);
            for(j=0;j<nCol;++j){
                grid.AddChild(new VSeparator());
                grid.AddChild(vals[i,j]);
            }
        }

        parent.AddChild(grid);
        initialized = true;
    }

    //------------------------------------------------------------------------
    // SetColLabel: Sets a column label string
    //------------------------------------------------------------------------
    public void SetColLabel(int idx, string str)
    {
        if(idx < 0 || idx >= nCol || !initialized)
        {
            return;
        }
        cLabels[idx].Text = str;
    }

    //------------------------------------------------------------------------
    // SetRowLabel: Sets a row label string
    //------------------------------------------------------------------------
    public void SetRowLabel(int idx, string str)
    {
        if(idx < 0 || idx >= nRow || !initialized)
        {
            return;
        }
        rLabels[idx].Text = str;
    }

    //------------------------------------------------------------------------
    // SetCornerLabel: sets label in top left corner
    //------------------------------------------------------------------------
    public void SetCornerLabel(string str)
    {
        if(!initialized)
            return;

        cornerLabel.Text = str;
    }
}